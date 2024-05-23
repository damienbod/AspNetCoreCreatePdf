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

        var document = new DocumentModel();

        var section = new Section(document);
        document.Sections.Add(section);

        var paragraph = new Paragraph(document);
        section.Blocks.Add(paragraph);

        var run = new Run(document, documentData.MainContentText);
        paragraph.Inlines.Add(run);
        return document;
    }

    private DocumentData GetDocumentData(string id, SaveOptions docType)
    {
        return new DocumentData
        {
            MainContentText = $"{docType.ContentType} created for id: {id}"
        };
    }
}
