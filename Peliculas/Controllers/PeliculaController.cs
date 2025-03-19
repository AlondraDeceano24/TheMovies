
using ML;
using Newtonsoft.Json;
using Peliculas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace Movies.Controllers
{
    public class PeliculaController : Controller
    {
        // GET: Peliculas
        
          
            public ActionResult GetAll()
            {
                ML.Pelis peliculas = new ML.Pelis();

                using (HttpClient client = new HttpClient())
                {
                    string URLMoviesPopular = ConfigurationManager.AppSettings["URLMoviesPopular"].ToString();
                    string TokenMoviesPopular = ConfigurationManager.AppSettings["TokenMoviesPopular"].ToString();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenMoviesPopular);

                    client.BaseAddress = new Uri(URLMoviesPopular);
                   
                    var responseTask = client.GetAsync("movie/popular");

                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Pelis>();
                        readTask.Wait();

                        var peliculasList = readTask.Result;//API
                        peliculas = peliculasList;

                    }
                }

                return View(peliculas);
            }
          
        public ActionResult FavoritasGetAll()
        {
            ML.Pelis peliculafav = new ML.Pelis();

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    string URLMoviesPopular = ConfigurationManager.AppSettings["URLMoviesPopular"].ToString();
                    string TokenMoviesPopular = ConfigurationManager.AppSettings["TokenMoviesPopular"].ToString();

                    client.BaseAddress = new Uri(URLMoviesPopular);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenMoviesPopular);
                  
                    var responseTask = client.GetAsync("account/21711153/favorite/movies");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode) {

                        var readTask = result.Content.ReadAsAsync<ML.Pelis>();
                        readTask.Wait();
                        peliculafav= readTask.Result;
                        TempData["Success"] = "Película agregada a favoritos.";
                    }
                    else
                    {
                        TempData["Error"] = "No se pudieron obtener las peliculas favoritas.";

                    }

                }
            }

            catch (Exception ex) {
                TempData["Error"] = $"Ocurrio un error:{ex.Message}";

            }
          return View(peliculafav);
        }


        public ActionResult AddFavoritas(int id)
        {
            ML.Pelis pelis = new ML.Pelis();
          

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URLMoviesPopular = ConfigurationManager.AppSettings["URLMoviesPopular"].ToString();
                    string TokenMoviesPopular = ConfigurationManager.AppSettings["TokenMoviesPopular"].ToString();

                    client.BaseAddress = new Uri(URLMoviesPopular);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenMoviesPopular);

                   
                    var data = new
                    {
                        media_type = "movie",
                        media_id = id,
                        favorite = true
                    };

                    // Serializar el objeto a JSON
                    var jsonData = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Hacer la solicitud POST
                    var responseTask = client.PostAsync("account/21711153/favorite", content);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Success"] = "Película agregada a favoritos.";
                    }
                  
                    {
                        TempData["Error"] = "No se pudo agregar la película a favoritos.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error: {ex.Message}";
            }
            return RedirectToAction("FavoritasGetAll");


        }

    }


}
