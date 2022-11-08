using apiWeb.DataConnection;
using apiWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEstudiantesController : ControllerBase
    {
        [HttpGet("Listar")]
        public async Task<IActionResult> GetEstudiantes()
        {
            return Ok(await Task.Run(() => new ListarEstudiantes().Listar()));
        }
        [HttpPut("actualizarEstudiante")]
        public async Task<IActionResult> ActualizarEstudiantes(RegistroEstudiantes reg)
        {
            return Ok(await Task.Run(() => new ListarEstudiantes().ActualizarEstudainte(reg)));
        }
        [HttpPost("registarEstudiante")]
        public async Task<IActionResult> RegistrarEstudiantes(RegistroEstudiantes reg)
        {
            return Ok(await Task.Run(() => new ListarEstudiantes().InsertarEstudiante(reg)));
        }
        [HttpDelete("eliminarEstudiante/{id}")]
        public async Task<IActionResult> EliminarEstudiante(int id)
        {
            return Ok(await Task.Run(() => new ListarEstudiantes().EliminarEstudiante(id)));
        }
    }
}
