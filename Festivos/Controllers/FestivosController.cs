using Festivos.Application.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Festivos.API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class FestivosController : ControllerBase
    {
        private readonly FestivosService _festivoService;
        public FestivosController(FestivosService festivosService)
        {
            _festivoService = festivosService;
        }

        [HttpGet]
        public async Task<IActionResult> EsFestivo([FromQuery] string date)
        {
            if (!DateTime.TryParse(date,out DateTime parsedDate))
            {
                return BadRequest("Formato de fecha erroneo");
            }
            var response = await _festivoService.esFestivo(parsedDate);
            if (response)
            {
                return Ok("Es festivo :p!");
            }
            else { return Ok("No es festivo :("); }
        }
    }
}
