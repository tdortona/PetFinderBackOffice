using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Services;
using PetFinderBackOffice.ViewModels;

namespace PetFinderBackOffice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class ConsultasWatsonController : Controller
    {
        private readonly ConsultasWatsonService consultasWatsonService = new ConsultasWatsonService();
        private readonly ImagenMascotaService imagenMascotaService = new ImagenMascotaService();
        private readonly LogErroresService logErroresService = new LogErroresService();

        [HttpGet("/api/ConsultasWatson/ConsultarEncontrados/{claseNombre}/{claseRaza}/{score}")]
        public IActionResult ConsultarEncontrados(string claseNombre, string claseRaza, string score)
        {
            try
            {
                claseNombre = claseNombre == "n|o" ? string.Empty : claseNombre;
                claseRaza = claseRaza == "n|o" ? string.Empty : claseRaza;

                var consultas = this.consultasWatsonService.ConsultarEncontrados(claseNombre, claseRaza, int.Parse(score));

                List<ResultadoBusqueda> resultados = new List<ResultadoBusqueda>();
                ImagenMascota img = new ImagenMascota();

                foreach (var item in consultas)
                {
                    img = this.imagenMascotaService.GetImagenMascota(item.IdImagen);

                    resultados.Add(new ResultadoBusqueda()
                    {
                        Clase = item.Clase,
                        Imagen =  img.ImagenPath,
                        Score = item.Score.Value,
                        IdUsuario = img.IdUsuario
                    });
                }

                return this.Ok(resultados);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }
    }
}