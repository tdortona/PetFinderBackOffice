using System;
using System.Collections.Generic;
using System.IO;
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
    public class UsuarioController : Controller
    {
        private readonly UsuarioService usuarioService = new UsuarioService();
        private readonly MascotaService mascotaService = new MascotaService();
        private readonly ImagenMascotaService imagenMascotaService = new ImagenMascotaService();
        private readonly LogErroresService logErroresService = new LogErroresService();
        private readonly string mascotasPath = AppContext.BaseDirectory + "Resources\\Img\\Mascotas\\";

        // GET api/[controller]/id
        [HttpGet("{id}")]
        public IActionResult BuscaUsuarioPorId(string id)
        {
            try
            {
                Usuario usuario = usuarioService.BuscaUsuarioPorId(id);
                return this.Ok(usuario);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        // GET: api/<controller>/TraerMisMascotas/{id}
        [HttpGet("/api/Usuario/TraerMisMascotas/{id}")]
        public IActionResult TraerMisMascotas(int id)
        {
            try
            {
                List<Mascota> misMascotas  = new List<Mascota>();
                List<MascotaViewModel> misMascotasViewModel = new List<MascotaViewModel>();
                string nombreImg;
                List<string> totalDeFotosMascota;
                misMascotas = usuarioService.TraerMisMascotas(id);

                foreach (Mascota mascota in misMascotas)
                {

                    nombreImg = mascotaService.TraerAvatarMascota(mascota.IdMascota);
                    totalDeFotosMascota = imagenMascotaService.TraerFotosMascota(mascota.IdMascota);
                    misMascotasViewModel.Add(new MascotaViewModel()
                    {
                        IdMascota = mascota.IdMascota,
                        IdUsuario = mascota.IdUsuario,
                        Nombre = mascota.Nombre,
                        Perdida = mascota.Perdida,
                        Avatar =  nombreImg,
                        DescripcionRaza = mascotaService.TraeDescripcionRaza(mascota.IdRaza),
                        Entrenado = totalDeFotosMascota.Count()
                    });
                }
                return this.Ok(misMascotasViewModel);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        // POST: api/<controller>/ValidarUsuario
        [HttpPost("/api/Usuario/ValidarUsuario")]
        public IActionResult ValidarUsuario([FromBody]UsuarioViewModel rdUser)
        {
            try
            {
                Usuario usuario = usuarioService.BuscaUsuarioPorId(rdUser.Id);
             
                if(usuario != null)
                {
                    return this.Ok(usuario);
                }
                else
                {
                    usuarioService.RegistrarUsuario(rdUser);
                    usuario = usuarioService.BuscaUsuarioPorId(rdUser.Id);
                    return this.Ok(usuario);
                }
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        [HttpGet("/api/Usuario/GetUsuarioContacto/{idUsuario}")]
        public IActionResult GetUsuarioContacto(int idUsuario)
        {
            try
            {
                var us = this.usuarioService.GetUsuarioContacto(idUsuario);

                ContactarUsuarioViewModel usVM = new ContactarUsuarioViewModel
                {
                    Email = us.Email,
                    Nombre = us.Nombre,
                    TelefonoContacto = us.TelefonoContacto
                };

                return this.Ok(usVM);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }
    }
}