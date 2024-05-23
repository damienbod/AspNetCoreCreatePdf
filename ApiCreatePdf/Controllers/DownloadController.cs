using Microsoft.AspNetCore.Mvc;

namespace ApiCreatePdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {

        [Route("pdf/{id}")]
        [HttpGet]
        public FileStreamResult Download(int id)
        {
            var path = "test\\test.pdf";
            var stream = new FileStream(path, FileMode.Open);
            return File(stream, "application/pdf");
        }
    }
}
