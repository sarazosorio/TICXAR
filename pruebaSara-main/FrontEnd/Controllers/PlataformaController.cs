using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class PlataformaController : Controller
    {
        String ApiConsumir = "https://localhost:7083/api/ApiDocentes/";
        async Task<List<RegistroDocentes>> ListarDocentes()
        {
            List<RegistroDocentes> auxiliar = new List<RegistroDocentes>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(ApiConsumir);
                HttpResponseMessage mensaje = await cliente.GetAsync("Listar");
                if (mensaje.IsSuccessStatusCode)
                {
                    string respuesta = await mensaje.Content.ReadAsStringAsync();
                    auxiliar = JsonConvert.DeserializeObject<List<RegistroDocentes>>(respuesta);
                }
            }
            return auxiliar;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.solicitudes = await ListarDocentes();
            ViewBag.titulo = "Agregar";
            return View(await Task.Run(() => new RegistroDocentes()));
        }
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.solicitudes = await ListarDocentes();
            ViewBag.titulo = "Actualizar";
            RegistroDocentes reg = new RegistroDocentes();
            foreach (RegistroDocentes bean in ViewBag.solicitudes)
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
        public async Task<IActionResult> Agregar(RegistroDocentes reg)
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
                HttpResponseMessage respuesta = await cliente.PostAsync("registarDocentes", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.solicitudes = await ListarDocentes();
            ViewBag.titulo = "Agregar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroDocentes()));
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(RegistroDocentes reg)
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
                HttpResponseMessage respuesta = await cliente.PutAsync("actualizarDocente", contenido);
                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.solicitudes = await ListarDocentes();
            ViewBag.titulo = "Actualizar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroDocentes()));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(ApiConsumir);
                HttpResponseMessage respuesta = await cliente.DeleteAsync(
                    "eliminarDocentes/" + id);

                if (respuesta.IsSuccessStatusCode)
                {
                    mensaje = await respuesta.Content.ReadAsStringAsync();
                }
            }
            //al finalizar refrescar la pagina
            ViewBag.mensaje = mensaje;
            ViewBag.solicitudes = await ListarDocentes();
            ViewBag.titulo = "Eliminar";
            //envio un nuevo Seller, GET
            return View("Index", await Task.Run(() => new RegistroDocentes()));           
   
        }
    }
}
