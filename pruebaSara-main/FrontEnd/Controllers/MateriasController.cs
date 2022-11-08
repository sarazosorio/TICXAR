using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class MateriasController : Controller
    {
        String ApiConsumir = "https://localhost:7083/api/ApiMaterias/";
        async Task<List<RegistroMaterias>> ListarMaterias()
        {
            List<RegistroMaterias> auxiliar = new List<RegistroMaterias>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(ApiConsumir);
                HttpResponseMessage mensaje = await cliente.GetAsync("Listar");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    auxiliar = JsonConvert.DeserializeObject<List<RegistroMaterias>>(respuesta);
                }
            }
            return auxiliar;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.materias = await ListarMaterias();
            ViewBag.titulo = "Agregar";
            return View(await Task.Run(() => new RegistroMaterias()));
        }
        public async Task<IActionResult> Editar(string id)
        {
            ViewBag.materias = await ListarMaterias();
            ViewBag.titulo = "Actualizar";
            RegistroMaterias reg = new RegistroMaterias();
            foreach (RegistroMaterias bean in ViewBag.materias)
            {
                if ((bean.IDMateria + "") == id)
                {
                    reg = bean;
                    break;
                }
            }
            return View("Index", await Task.Run(() => reg));
        }



        [HttpPost]
        public async Task<IActionResult> Agregar(RegistroMaterias reg)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                
 
                cliente.BaseAddress = new Uri(ApiConsumir);
                //convierto a reg en un cadena json de formato utf-8 y media: applicacion/json
                StringContent contenido = new StringContent(
                JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");
                //ejecutar el Put
                HttpResponseMessage respuesta = await cliente.PostAsync("registarMateria", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.materias = await ListarMaterias();
            ViewBag.titulo = "Agregar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroMaterias()));
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(RegistroMaterias reg)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                
                cliente.BaseAddress = new Uri(ApiConsumir);
                //convierto a reg en un cadena json de formato utf-8 y media: applicacion/json
                StringContent contenido = new StringContent(
                JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");
                //ejecutar el Put
                HttpResponseMessage respuesta = await cliente.PutAsync("actualizarMateria", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.materias = await ListarMaterias();
            ViewBag.titulo = "Actualizar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroMaterias()));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(ApiConsumir);
                HttpResponseMessage respuesta = await cliente.DeleteAsync(
                    "eliminarMateria/" + id);

                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.materias = await ListarMaterias();
            ViewBag.titulo = "Eliminar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroMaterias()));

        }

    }
}
