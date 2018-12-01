﻿using System;
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

        [HttpGet("/api/ConsultasWatson/ConsultarEncontrados/{claseNombre}/{claseRaza}/{score}")]
        public IActionResult ConsultarEncontrados(string claseNombre, string claseRaza, string score)
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
                    Imagen = "data:image/jpeg;base64," + this.imagenMascotaService.GetImagen(img.IdMascota.Value, img.ImagenPath),
                    Score = item.Score.Value,
                    IdUsuario = img.IdUsuario
                });
            }

            return this.Ok(resultados);
        }
    }
}