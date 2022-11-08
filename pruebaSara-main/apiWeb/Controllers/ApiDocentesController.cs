using apiWeb.DataConnection;
using apiWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiDocentesController : ControllerBase
    {
        [HttpGet("Listar")]
        public async Task<IActionResult> GetDocentes()
        {
            return Ok(await Task.Run(() => new ListarDocentes().Listar()));
        }
        [HttpPut("actualizarDocente")]
        public async Task<IActionResult> ActualizarDocentes(RegistroDocentes reg)
        {
            return Ok(await Task.Run(() => new ListarDocentes().ActualizarDocente(reg)));
        }
        [HttpPost("registarDocentes")]
        public async Task<IActionResult> RegistrarDocentes(RegistroDocentes reg)
        {
            return Ok(await Task.Run(() => new ListarDocentes().InsertarDocentes(reg)));
        }
        [HttpDelete("eliminarDocentes/{id}")]
        public async Task<IActionResult> EliminarDocentes(int id)
        {
            return Ok(await Task.Run(() => new ListarDocentes().EliminarDocente(id)));
        }
    }
}
