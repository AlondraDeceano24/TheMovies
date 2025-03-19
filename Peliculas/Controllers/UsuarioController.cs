using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Peliculas.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult GetAll()
        {
            ML.Users users = new ML.Users();
            using (HttpClient client = new HttpClient())
            {
                string URLUsuariosRandom = ConfigurationManager.AppSettings["URLUsuariosRandom"].ToString();
                
                client.BaseAddress = new Uri(URLUsuariosRandom);
                // HTTP GET
                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Users>();
                    readTask.Wait();

                    users = readTask.Result;
                }
            }
            return View(users);
        }

        // Acción para obtener más usuarios (JSON)
        [HttpGet]
        public JsonResult GetMoreUsers()
        {
            ML.Users users = new ML.Users();
            using (HttpClient client = new HttpClient())
            {
                string URLUsuariosRandom = ConfigurationManager.AppSettings["URLUsuariosRandom"].ToString();

                client.BaseAddress = new Uri(URLUsuariosRandom);
                var responseTask = client.GetAsync("");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Users>();
                    readTask.Wait();
                    users = readTask.Result;
                }
            }
            return Json(users.results, JsonRequestBehavior.AllowGet);
        }
    }

}