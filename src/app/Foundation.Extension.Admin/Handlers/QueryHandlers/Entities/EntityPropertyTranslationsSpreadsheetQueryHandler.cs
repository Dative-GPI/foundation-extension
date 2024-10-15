using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using Bones.Flow;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class EntityPropertyTranslationsSpreadsheetQueryHandler : IMiddleware<EntityPropertyTranslationsSpreadsheetQuery, byte[]>
    {
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IFoundationClientFactory _foundationClientFactory;
        private readonly IEntityPropertyRepository _entityPropertyRepository;
        private readonly IEntityPropertyApplicationTranslationRepository _entityPropertyTranslationRepository;

        public EntityPropertyTranslationsSpreadsheetQueryHandler
        (
            IRequestContextProvider requestContextProvider,
            IFoundationClientFactory foundationClientFactory,
            IEntityPropertyRepository entityPropertyRepository,
            IEntityPropertyApplicationTranslationRepository entityPropertyTranslationRepository
        )
        {
            _requestContextProvider = requestContextProvider;
            _foundationClientFactory = foundationClientFactory;
            _entityPropertyRepository = entityPropertyRepository;
            _entityPropertyTranslationRepository = entityPropertyTranslationRepository;
        }

        public async Task<byte[]> HandleAsync(EntityPropertyTranslationsSpreadsheetQuery command, Func<Task<byte[]>> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            var adminFoundationClient = await _foundationClientFactory.CreateAdmin(context.ApplicationId, context.LanguageCode);

            // Get all existing entityProperties
            var entityProperties = await _entityPropertyRepository.GetMany(new EntityPropertiesFilter());

            // Get all languages for this application
            var applicationLanguages = await adminFoundationClient.Admin.ApplicationLanguages.GetMany();

            // Get all entityProperties for this application
            var entityPropertyTranslations = await _entityPropertyTranslationRepository.GetMany(new EntityPropertyApplicationTranslationsFilter()
            {
                ApplicationId = command.ApplicationId
            });

            using var tmp = new MemoryStream();

            // Create all required parts
            using (var xlsx = SpreadsheetDocument.Create(tmp, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = xlsx.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = sheets.AppendChild(new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "EntityProperties",
                    State = SheetStateValues.Visible
                });
                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Add headers in first row
                var headers = sheetData.AppendChild(new Row());
                headers.AppendChild(new Cell()
                {
                    CellReference = "A1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("Code")
                });
                headers.AppendChild(new Cell()
                {
                    CellReference = "B1",
                    DataType = CellValues.String,
                    CellValue = new CellValue("Default label")
                });

                var index = 2;
                foreach (var language in applicationLanguages)
                {
                    headers.AppendChild(new Cell()
                    {
                        CellReference = $"{ColumnIndexToCellReference(index)}1",
                        DataType = CellValues.String,
                        CellValue = new CellValue($"{language.Code} - label")
                    });
                    headers.AppendChild(new Cell()
                    {
                        CellReference = $"{ColumnIndexToCellReference(index + 1)}1",
                        DataType = CellValues.String,
                        CellValue = new CellValue($"{language.Code} - application label")
                    });
                    index += 2;
                }

                // Add entityProperties in other rows
                foreach (var entityProperty in entityProperties)
                {
                    var row = sheetData.AppendChild(new Row());
                    row.AppendChild(new Cell()
                    {
                        CellReference = $"A{sheetData.ChildElements.Count}",
                        DataType = CellValues.String,
                        CellValue = new CellValue(entityProperty.Code)
                    });
                    row.AppendChild(new Cell()
                    {
                        CellReference = $"B{sheetData.ChildElements.Count}",
                        DataType = CellValues.String,
                        CellValue = new CellValue(entityProperty.LabelDefault)
                    });
                    index = 2;

                    foreach (var language in applicationLanguages)
                    {
                        var entityPropertyTranslation = entityProperty.Translations
                            .FirstOrDefault(t => t.LanguageCode == language.Code);

                        if (entityPropertyTranslation != null)
                        {
                            row.AppendChild(new Cell()
                            {
                                CellReference = $"{ColumnIndexToCellReference(index)}{sheetData.ChildElements.Count}",
                                DataType = CellValues.String,
                                CellValue = new CellValue(entityPropertyTranslation.Label)
                            });
                        }
                        index++;

                        var entityPropertyApplicationTranslation = entityPropertyTranslations
                            .FirstOrDefault(t => t.EntityPropertyId == entityProperty.Id && t.LanguageCode == language.Code);

                        if (entityPropertyApplicationTranslation != null)
                        {
                            row.AppendChild(new Cell()
                            {
                                CellReference = $"{ColumnIndexToCellReference(index)}{sheetData.ChildElements.Count}",
                                DataType = CellValues.String,
                                CellValue = new CellValue(entityPropertyApplicationTranslation.Label)
                            });
                        }
                        index++;
                    }
                }
            }

            return tmp.ToArray();
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