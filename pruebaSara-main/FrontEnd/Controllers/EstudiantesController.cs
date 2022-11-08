using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class EstudiantesController : Controller
    {
        String ApiConsumir = "https://localhost:7083/api/ApiEstudiantes/";
        async Task<List<RegistroEstudiantes>> ListarEstudiantes()
        {
            List<RegistroEstudiantes> auxiliar = new List<RegistroEstudiantes>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(ApiConsumir);
                HttpResponseMessage mensaje = await cliente.GetAsync("Listar");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    auxiliar = JsonConvert.DeserializeObject<List<RegistroEstudiantes>>(respuesta);
                }
            }
            return auxiliar;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.estudiantes = await ListarEstudiantes();
            ViewBag.titulo = "Agregar";
            return View(await Task.Run(() => new RegistroEstudiantes()));
        }
        public async Task<IActionResult> Editar(string id)
        {
            ViewBag.estudiantes = await ListarEstudiantes();
            ViewBag.titulo = "Actualizar";
            RegistroEstudiantes reg = new RegistroEstudiantes();
            foreach (RegistroEstudiantes bean in ViewBag.estudiantes)
            {
                if ((bean.Id + "") == id)
                {
                    reg = bean;
                    break;
                }
            }
            return View("Index", await Task.Run(() => reg));
        }



        [HttpPost]
        public async Task<IActionResult> Agregar(RegistroEstudiantes reg)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                reg.FechaNacimiento = DateTime.Now;
 
                cliente.BaseAddress = new Uri(ApiConsumir);
                //convierto a reg en un cadena json de formato utf-8 y media: applicacion/json
                StringContent contenido = new StringContent(
                JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");
                //ejecutar el Put
                HttpResponseMessage respuesta = await cliente.PostAsync("registarEstudiante", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.estudiantes = await ListarEstudiantes();
            ViewBag.titulo = "Agregar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroEstudiantes()));
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(RegistroEstudiantes reg)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                reg.FechaNacimiento = DateTime.Now;
                cliente.BaseAddress = new Uri(ApiConsumir);
                //convierto a reg en un cadena json de formato utf-8 y media: applicacion/json
                StringContent contenido = new StringContent(
                JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");
                //ejecutar el Put
                HttpResponseMessage respuesta = await cliente.PutAsync("actualizarEstudiante", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.estudiantes = await ListarEstudiantes();
            ViewBag.titulo = "Actualizar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroEstudiantes()));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(ApiConsumir);
                HttpResponseMessage respuesta = await cliente.DeleteAsync(
                    "eliminarEstudiante/" + id);

                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.estudiantes = await ListarEstudiantes();
            ViewBag.titulo = "Eliminar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroEstudiantes()));

        }

        public IActionResult Index2()
        {
            return View();
        }

    }
}
