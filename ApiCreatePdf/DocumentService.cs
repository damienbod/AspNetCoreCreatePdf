using GemBox.Document;

namespace ApiCreatePdf;

public class DocumentService
{
    public Stream GeneratePdf(string id)
    {
        var documentData = GetDocumentData(id);

        var pdf = new MemoryStream();

        // If using the Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        var document = new DocumentModel();

        var section = new Section(document);
        document.Sections.Add(section);

        var paragraph = new Paragraph(document);
        section.Blocks.Add(paragraph);

        var run = new Run(document, documentData.MainContentText);
        paragraph.Inlines.Add(run);

        document.Save(pdf, SaveOptions.PdfDefault);

        return pdf;
    }

    private DocumentData GetDocumentData(string id)
    {
        return new DocumentData
        {
            MainContentText = $"PDF created for id: {id}"
        };
    }
}
