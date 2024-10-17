using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using Bones.Flow;

using Foundation.Extension.Admin.Requests;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;


namespace Foundation.Extension.Admin.Handlers
{
    public class UploadApplicationTranslationsCommandHandler : IMiddleware<UploadApplicationTranslationsCommand>
    {
        private readonly ITranslationRepository _translationRepository;
        private readonly IApplicationTranslationRepository _applicationTranslationRepository;

        public UploadApplicationTranslationsCommandHandler
        (
            ITranslationRepository translationRepository,
            IApplicationTranslationRepository applicationTranslationRepository
        )
        {
            _translationRepository = translationRepository;
            _applicationTranslationRepository = applicationTranslationRepository;
        }

        public async Task HandleAsync(UploadApplicationTranslationsCommand command, Func<Task> next, CancellationToken cancellationToken)
        {
            // Get all existing translations
            var translations = await _translationRepository.GetMany();

            // Remove all existing translations for each uploaded language for this application
            var applicationTranslations = await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = command.ApplicationId
            });

            await _applicationTranslationRepository.RemoveRange(applicationTranslations
                .Where(at => command.Languages.Any(l => l.LanguageCode == at.LanguageCode))
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

                var commands = new List<CreateApplicationTranslation>();

                // Skip the first row, it is supposed to be the header
                foreach (var row in rows.Skip(1))
                {
                    // First cell contains the translation code
                    var codeCell = row.Descendants<Cell>().First();

                    if (codeCell == default)
                    {
                        // If the first cell doesn't contain anything, we skip the row (no Code)
                        continue;
                    }

                    // Check if the translation exists
                    var code = (((codeCell.DataType ?? CellValues.String) == CellValues.SharedString) && strings != null) ?
                        strings.ElementAt(Convert.ToInt32(codeCell.InnerText)).InnerText : codeCell.InnerText;

                    var translation = translations.FirstOrDefault(t => t.Code == code);

                    if (translation == default)
                    {
                        continue;
                    }

                    foreach (var language in command.Languages)
                    {
                        // This cell is supposed to contain the translation for this language
                        var cellReference = $"{ColumnIndexToCellReference(language.Index)}{CellReferenceToRowIndex(codeCell.CellReference)}";
                        var translationCell = row.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference);

                        if (translationCell == default)
                        {
                            continue;
                        }

                        var value = (((translationCell.DataType ?? CellValues.String) == CellValues.SharedString) && strings != null) ?
                            strings.ElementAt(Convert.ToInt32(translationCell.InnerText)).InnerText : translationCell.InnerText;

                        commands.Add(new CreateApplicationTranslation()
                        {
                            ApplicationId = command.ApplicationId,
                            LanguageCode = language.LanguageCode,
                            TranslationId = translation.Id,
                            Value = value
                        });
                    }
                }

                await _applicationTranslationRepository.CreateRange(commands);
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