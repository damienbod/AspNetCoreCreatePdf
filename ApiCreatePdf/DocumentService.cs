using GemBox.Document;

namespace ApiCreatePdf;

public class DocumentService
{
    public Stream GeneratePdf(string id)
    {
        var documentData = GetDocumentData(id, SaveOptions.PdfDefault);

        var pdf = new MemoryStream();
        var document = CreateDocument(documentData);

        document.Save(pdf, SaveOptions.PdfDefault);

        return pdf;
    }

    public Stream GenerateDocx(string id)
    {
        var documentData = GetDocumentData(id, SaveOptions.DocxDefault);

        var docx = new MemoryStream();
        var document = CreateDocument(documentData);

        document.Save(docx, SaveOptions.DocxDefault);

        return docx;
    }

    private static DocumentModel CreateDocument(DocumentData documentData)
    {
        // If using the Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        ComponentInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;

        var document = new DocumentModel();

        var section = new Section(document);
        document.Sections.Add(section);

        // Main text 
        var paragraph = new Paragraph(document);
        section.Blocks.Add(paragraph);
        var run = new Run(document, documentData.MainContentText);
        paragraph.Inlines.Add(run);

        var bookmarkName = "TopOfDocument";

        document.Sections.Add(
            new Section(document,
                new Paragraph(document,
                    new BookmarkStart(document, bookmarkName),
                    new Run(document, "This is a 'TopOfDocument' bookmark."),
                    new BookmarkEnd(document, bookmarkName)),
                new Paragraph(document,
                    new Run(document, "The following is a link to "),
                    new Hyperlink(document, "https://www.gemboxsoftware.com/document", "GemBox.Document Overview"),
                    new Run(document, " page.")),
                 new Paragraph(document,
                    new SpecialCharacter(document, SpecialCharacterType.PageBreak),
                    new Run(document, "This is a document's second page."),
                    new SpecialCharacter(document, SpecialCharacterType.LineBreak),
                    new Hyperlink(document, bookmarkName, "Return to 'TopOfDocument'.") { IsBookmarkLink = true })));

        return document;
    }

    private static DocumentData GetDocumentData(string id, SaveOptions docType)
    {
        return new DocumentData
        {
            MainContentText = $"{docType.ContentType} created for id: {id}"
        };
    }
}
