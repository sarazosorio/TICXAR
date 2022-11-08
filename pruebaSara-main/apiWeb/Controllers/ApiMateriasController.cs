using apiWeb.DataConnection;
using apiWeb.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMateriasController : ControllerBase
    {
        [HttpGet("Listar")]
        public async Task<IActionResult> GetMaterias()
        {
            return Ok(await Task.Run(() => new ListarMateria().Listar()));
        }
        [HttpPut("actualizarMateria")]
        public async Task<IActionResult> ActualizarMaterias(RegistroMateria reg)
        {
            return Ok(await Task.Run(() => new ListarMateria().ActualizarMateria(reg)));
        }
        [HttpPost("registarMateria")]
        public async Task<IActionResult> RegistrarMaterias(RegistroMateria reg)
        {
            return Ok(await Task.Run(() => new ListarMateria().InsertarMateria(reg)));
        }
        [HttpDelete("eliminarMateria/{id}")]
        public async Task<IActionResult> EliminarMateria(int id)
        {
            return Ok(await Task.Run(() => new ListarMateria().EliminarMateria(id)));
        }
    }
}
