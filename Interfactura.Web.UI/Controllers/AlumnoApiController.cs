using Interfactura.Web.UI.Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interfactura.Web.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlumnoApiController : ControllerBase
    {
        [HttpPost]
        [Route("uploadXml")]
        public IActionResult PostXML([FromForm(Name = "xmlFile")] IFormFile xmlFile, string rfcAlumno)
        {
            if (xmlFile.ContentType != "application/xml") return BadRequest("Tipo de archivo no soportado");
            if (xmlFile.Length <= 0) return BadRequest("Tamaño de archivo no soportado");

            string resultado = ProcessXMLFile.ValidateXML(xmlFile, rfcAlumno);

            if(string.IsNullOrWhiteSpace(resultado)) return Ok();
            return BadRequest(new { Error = resultado });
        }
    }
}