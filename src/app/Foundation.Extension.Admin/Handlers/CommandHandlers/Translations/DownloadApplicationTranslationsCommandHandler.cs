using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using Bones.Flow;

using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Admin.Requests;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.Handlers
{
    public class DownloadApplicationTranslationsCommandHandler : IMiddleware<DownloadApplicationTranslationsCommand>
    {
        private readonly ITranslationRepository _translationRepository;
        private readonly IFoundationClientFactory _foundationClientFactory;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IApplicationTranslationRepository _applicationTranslationRepository;

        public DownloadApplicationTranslationsCommandHandler
        (
            ITranslationRepository translationRepository,
            IFoundationClientFactory foundationClientFactory,
            IRequestContextProvider requestContextProvider,
            IApplicationTranslationRepository applicationTranslationRepository
        )
        {
            _translationRepository = translationRepository;
            _foundationClientFactory = foundationClientFactory;
            _requestContextProvider = requestContextProvider;
            _applicationTranslationRepository = applicationTranslationRepository;
        }

        public async Task HandleAsync(DownloadApplicationTranslationsCommand command, Func<Task> next, CancellationToken cancellationToken)
        {
            // Get all existing translations
            var translations = await _translationRepository.GetMany();

            var context = _requestContextProvider.Context;

            var adminFoundationClient = await _foundationClientFactory.CreateAdmin(context.ApplicationId, context.LanguageCode);



            // Get all languages for this application
            var applicationLanguages = await adminFoundationClient.Admin.ApplicationLanguages.GetMany();
            // Get all translations for this application
            var applicationTranslations = await _applicationTranslationRepository.GetMany(new ApplicationTranslationsFilter()
            {
                ApplicationId = command.ApplicationId
            });

            // Create all required parts
            using (var xlsx = SpreadsheetDocument.Create(command.File, SpreadsheetDocumentType.Workbook))
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
                        CellValue = new CellValue(language.Code)
                    });
                    index++;
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