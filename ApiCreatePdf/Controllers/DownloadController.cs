using Microsoft.AspNetCore.Mvc;

namespace ApiCreatePdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController(DocumentService _documentService) : ControllerBase
    {
        [Route("pdf/{id}")]
        [HttpGet]
        public FileStreamResult DownloadPdf(string id)
        {
            var stream = _documentService.GeneratePdf(id);
            return File(stream, "application/pdf");
        }

        [Route("docx/{id}")]
        [HttpGet]
        public FileStreamResult DownloadDocx(string id)
        {
            var stream = _documentService.GenerateDocx(id);
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        }
    }
}
