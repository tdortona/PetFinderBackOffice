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

        // GET: api/<controller>/TraerMascota/{id}
        [HttpGet("/api/Mascota/TraerMascota/{id}")]
        public IActionResult TraerMascota(int id)
        {
            Mascota mascota = mascotaService.TraerMascota(id);
            MascotaViewModel mascotaViewModel = new MascotaViewModel
            {
                IdMascota = mascota.IdMascota,
                Nombre = mascota.Nombre,
                DescripcionRaza = mascotaService.TraeDescripcionRaza(mascota.IdRaza),
                Avatar = mascotaService.TraerAvatarMascota(mascota.IdMascota),
                Perdida = mascota.Perdida
            };
            return this.Ok(mascotaViewModel);
        }

        // POST: api/<controller>/ReportarPerdida
        [HttpPost("/api/Mascota/ReportarPerdida")]
        public IActionResult ReportarPerdida([FromBody] MascotaPerdidaViewModel mascota)
        {
            mascotaService.ReportarPerdida(mascota.IdMascota);
            return this.Ok();
        }

        // POST: api/<controller>/ReportarEncontrada
        [HttpPost("/api/Mascota/ReportarEncontrada")]
        public IActionResult ReportarEncontrada([FromBody] MascotaPerdidaViewModel mascota)
        {
            mascotaService.ReportarEncontrada(mascota.IdMascota);
            return this.Ok();
        }
    }
}