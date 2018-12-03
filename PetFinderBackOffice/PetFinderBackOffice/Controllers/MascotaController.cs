using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Services;
using PetFinderBackOffice.ViewModels;

namespace PetFinderBackOffice.Controllers
{
    public class MascotaController : Controller
    {
        private readonly MascotaService mascotaService = new MascotaService();
        private readonly ImagenMascotaService imagenMascotaService = new ImagenMascotaService();
        private readonly LogErroresService logErroresService = new LogErroresService();

        // GET: api/<controller>/TraerMascota/{id}
        [HttpGet("/api/Mascota/TraerMascota/{id}")]
        public IActionResult TraerMascota(int id)
        {
            try
            {
                Mascota mascota = mascotaService.TraerMascota(id);
                string nombreImg = mascotaService.TraerAvatarMascota(mascota.IdMascota);

                MascotaViewModel mascotaViewModel = new MascotaViewModel
                {
                    IdMascota = mascota.IdMascota,
                    Nombre = mascota.Nombre,
                    DescripcionRaza = mascotaService.TraeDescripcionRaza(mascota.IdRaza),
                    Avatar = nombreImg,
                    Perdida = mascota.Perdida
                };

                return this.Ok(mascotaViewModel);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        // POST: api/<controller>/ReportarPerdida
        [HttpPost("/api/Mascota/ReportarPerdida")]
        public IActionResult ReportarPerdida([FromBody] MascotaPerdidaViewModel mascota)
        {
            try
            {
                mascotaService.ReportarPerdida(mascota.IdMascota);
                return this.Ok();
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        // POST: api/<controller>/ReportarEncontrada
        [HttpPost("/api/Mascota/ReportarEncontrada")]
        public IActionResult ReportarEncontrada([FromBody] MascotaPerdidaViewModel mascota)
        {
            try
            {
                mascotaService.ReportarEncontrada(mascota.IdMascota);
                return this.Ok();
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        // POST: api/<controller>/AgregarMascotaNueva
        [HttpPost("/api/Mascota/AgregarMascotaNueva")]
        public IActionResult AgregarMascotaNueva([FromBody] MascotaNuevaViewModel mascota)
        {
            try
            {
                Mascota mascotaNueva = new Mascota
                {
                    IdUsuario = mascota.IdUsuario,
                    Nombre = mascota.Nombre,
                    IdRaza = mascota.IdRaza
                };

                mascotaService.AgregarMascotaNueva(mascotaNueva);

                return this.Ok();
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        // GET: api/<controller>/TraerRazas
        [HttpGet("/api/Mascota/TraerRazas")]
        public IActionResult TraerRazas()
        {
            try
            {
                List<Raza> listaRazas = new List<Raza>();
                listaRazas = mascotaService.TraerRazas();
                return this.Ok(listaRazas);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }
    }
}