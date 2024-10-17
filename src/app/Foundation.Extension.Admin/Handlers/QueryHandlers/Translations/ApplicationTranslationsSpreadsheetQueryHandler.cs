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
using Foundation.Extension.Admin.Requests;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class ApplicationTranslationsSpreadSheetQueryHandler : IMiddleware<ApplicationTranslationsSpreadsheetQuery, byte[]>
    {
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IFoundationClientFactory _foundationClientFactory;
        private readonly ITranslationRepository _translationRepository;
        private readonly IApplicationTranslationRepository _applicationTranslationRepository;

        public ApplicationTranslationsSpreadSheetQueryHandler
        (
            IRequestContextProvider requestContextProvider,
            IFoundationClientFactory foundationClientFactory,
            ITranslationRepository translationRepository,
            IApplicationTranslationRepository applicationTranslationRepository
        )
        {
            _requestContextProvider = requestContextProvider;
            _foundationClientFactory = foundationClientFactory;
            _translationRepository = translationRepository;
            _applicationTranslationRepository = applicationTranslationRepository;
        }

        public async Task<byte[]> HandleAsync(ApplicationTranslationsSpreadsheetQuery request, Func<Task<byte[]>> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            var adminFoundationClient = await _foundationClientFactory.CreateAdmin(context.ApplicationId, context.LanguageCode);

            // Get all existing translations
            var translations = await _translationRepository.GetMany();

            // Get all languages for this application
            var applicationLanguages = await adminFoundationClient.Admin.ApplicationLanguages.GetMany();

            // Get all translations for this application
            var applicationTranslations = await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = request.ApplicationId
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
                    Name = "Translations",
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
                    CellValue = new CellValue("Default value")
                });

                var index = 2;
                foreach (var language in applicationLanguages)
                {
                    headers.AppendChild(new Cell()
                    {
                        CellReference = $"{ColumnIndexToCellReference(index)}1",
                        DataType = CellValues.String,
                        CellValue = new CellValue($"Default {language.Code}")
                    });
                    headers.AppendChild(new Cell()
                    {
                        CellReference = $"{ColumnIndexToCellReference(index + 1)}1",
                        DataType = CellValues.String,
                        CellValue = new CellValue($"Application {language.Code}")
                    });
                    index += 2;
                }

                // Add translations in other rows
                foreach (var translation in translations)
                {
                    var row = sheetData.AppendChild(new Row());
                    row.AppendChild(new Cell()
                    {
                        CellReference = $"A{sheetData.ChildElements.Count}",
                        DataType = CellValues.String,
                        CellValue = new CellValue(translation.Code)
                    });
                    row.AppendChild(new Cell()
                    {
                        CellReference = $"B{sheetData.ChildElements.Count}",
                        DataType = CellValues.String,
                        CellValue = new CellValue(translation.Value)
                    });
                    index = 2;

                    foreach (var language in applicationLanguages)
                    {
                        var translationTranslation = translation.Translations
                            .FirstOrDefault(tt => tt.LanguageCode == language.Code);

                        if (translationTranslation != null)
                        {
                            row.AppendChild(new Cell()
                            {
                                CellReference = $"{ColumnIndexToCellReference(index)}{sheetData.ChildElements.Count}",
                                DataType = CellValues.String,
                                CellValue = new CellValue(translationTranslation.Value)
                            });
                        }
                        index++;

                        var applicationTranslation = applicationTranslations
                            .FirstOrDefault(at => at.TranslationCode == translation.Code && at.LanguageCode == language.Code);
                        
                        if (applicationTranslation != null)
                        {
                            row.AppendChild(new Cell()
                            {
                                CellReference = $"{ColumnIndexToCellReference(index)}{sheetData.ChildElements.Count}",
                                DataType = CellValues.String,
                                CellValue = new CellValue(applicationTranslation.Value)
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