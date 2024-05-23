using Microsoft.AspNetCore.Mvc;

namespace ApiCreatePdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController(DocumentService _documentService) : ControllerBase
    {
        [Route("pdf/{id}")]
        [HttpGet]
        public FileStreamResult Download(string id)
        {
            var stream = _documentService.GeneratePdf(id);
            return File(stream, "application/pdf");
        }
    }
}
