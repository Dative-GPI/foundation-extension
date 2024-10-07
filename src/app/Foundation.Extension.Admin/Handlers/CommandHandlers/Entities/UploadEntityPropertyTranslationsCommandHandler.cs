using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using Bones.Flow;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class UploadEntityPropertyTranslationsCommandHandler : IMiddleware<UploadEntityPropertyTranslationsCommand>
    {
        private readonly IEntityPropertyRepository _entityPropertyRepository;
        private readonly IEntityPropertyApplicationTranslationRepository _entityPropertyTranslationRepository;

        public UploadEntityPropertyTranslationsCommandHandler
        (
            IEntityPropertyRepository entityPropertyRepository,
            IEntityPropertyApplicationTranslationRepository entityPropertyTranslationRepository
        )
        {
            _entityPropertyRepository = entityPropertyRepository;
            _entityPropertyTranslationRepository = entityPropertyTranslationRepository;
        }

        public async Task HandleAsync(UploadEntityPropertyTranslationsCommand command, Func<Task> next, CancellationToken cancellationToken)
        {
            // Get all existing entityPropertys
            var entityPropertys = await _entityPropertyRepository.GetMany(new EntityPropertiesFilter());

            // Remove all existing entityPropertys for each uploaded language for this application
            var updatedLanguages = command.Labels.Select(l => l.LanguageCode).Concat(command.Categories.Select(l => l.LanguageCode).Distinct()).ToHashSet();
            var languageConfiguration = command.Labels.Join(command.Categories, l => l.LanguageCode, c => c.LanguageCode,
                (l, c) => new { l.LanguageCode, LabelIndex = l.Index, CategoryIndex = c.Index })
                .ToDictionary(l => l.LanguageCode);

            var entityPropertyTranslations = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyApplicationTranslationsFilter()
            {
                ApplicationId = command.ApplicationId
            });

            await _entityPropertyTranslationRepository.RemoveRange(entityPropertyTranslations
                .Where(et => updatedLanguages.Contains(et.LanguageCode))
                .Select(at => at.Id)
            );

            // Handle the xlsx file
            using (var xlsx = SpreadsheetDocument.Open(command.File, false))
            {
                var sheet = ((WorksheetPart)xlsx.WorkbookPart.GetPartById(xlsx.WorkbookPart.Workbook
                    .Descendants<Sheet>()
                    .FirstOrDefault(s => (s.State ?? SheetStateValues.Visible) == SheetStateValues.Visible)?.Id))?.Worksheet;

                if (sheet == default)
                {
                    return;
                }

                var strings = xlsx.WorkbookPart.SharedStringTablePart?.SharedStringTable.Elements<SharedStringItem>();
                var rows = sheet.Descendants<Row>().ToList();

                // We need at least 2 rows, first one contains the headers
                if (rows.Count < 2)
                {
                    return;
                }

                var commands = new List<CreateEntityPropertyApplicationTranslation>();

                // Skip the first row, it is supposed to be the header
                foreach (var row in rows.Skip(1))
                {
                    // First cell contains the entityProperty code
                    var codeCell = row.Descendants<Cell>().First();

                    if (codeCell == default)
                    {
                        // If the first cell doesn't contain anything, we skip the row (no Code)
                        continue;
                    }

                    // Check if the entityProperty exists
                    var code = (((codeCell.DataType ?? CellValues.String) == CellValues.SharedString) && strings != null) ?
                        strings.ElementAt(Convert.ToInt32(codeCell.InnerText)).InnerText : codeCell.InnerText;

                    var entityProperty = entityPropertys.FirstOrDefault(t => t.Code == code);

                    if (entityProperty == default)
                    {
                        continue;
                    }

                    foreach (var language in languageConfiguration)
                    {
                        // This cell is supposed to contain the entityProperty for this language
                        var labelCellReference = $"{ColumnIndexToCellReference(language.Value.LabelIndex)}{CellReferenceToRowIndex(codeCell.CellReference)}";
                        var labelCell = row.Descendants<Cell>().FirstOrDefault(c => c.CellReference == labelCellReference);

                        var categoryCellReference = $"{ColumnIndexToCellReference(language.Value.CategoryIndex)}{CellReferenceToRowIndex(codeCell.CellReference)}";
                        var categoryCell = row.Descendants<Cell>().FirstOrDefault(c => c.CellReference == categoryCellReference);

                        if (labelCell == default && categoryCell == default)
                        {
                            continue;
                        }

                        var label = (labelCell != null && ((labelCell.DataType ?? CellValues.String) == CellValues.SharedString) && strings != null) ?
                            strings.ElementAt(Convert.ToInt32(labelCell.InnerText)).InnerText : labelCell?.InnerText;

                        var category = (categoryCell != null && ((categoryCell.DataType ?? CellValues.String) == CellValues.SharedString) && strings != null) ?
                            strings.ElementAt(Convert.ToInt32(categoryCell.InnerText)).InnerText : categoryCell?.InnerText;

                        commands.Add(new CreateEntityPropertyApplicationTranslation()
                        {
                            ApplicationId = command.ApplicationId,
                            LanguageCode = language.Value.LanguageCode,
                            EntityPropertyId = entityProperty.Id,
                            Label = label,
                            CategoryLabel = category
                        });
                    }
                }

                await _entityPropertyTranslationRepository.CreateRange(commands);
            }
        }

        private static int CellReferenceToRowIndex(string reference)
        {
            var rowIndex = 0;
            foreach (var c in reference)
            {
                if (c >= '0' && c <= '9')
                {
                    rowIndex = rowIndex * 10 + (c - '0');
                }
            }
            return rowIndex;
        }

        private static string ColumnIndexToCellReference(int index)
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var column = "";
            while (index >= 0)
            {
                column = letters[index % 26] + column;
                index = (int)(Math.Floor((decimal)(index / 26)) - 1);
            }
            return column;
        }
    }
}