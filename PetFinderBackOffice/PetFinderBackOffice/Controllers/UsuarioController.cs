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
    public class UsuarioController : Controller
    {
        private readonly UsuarioService usuarioService = new UsuarioService();
        private readonly MascotaService mascotaService = new MascotaService();

        // GET api/[controller]/id
        [HttpGet("{id}")]
        public IActionResult BuscaUsuarioPorId(string id)
        {
            Usuario usuario = usuarioService.BuscaUsuarioPorId(id);
            return this.Ok(usuario);
        }


        // GET: api/<controller>/TraerMisMascotas/{id}
        [HttpGet("/api/Usuario/TraerMisMascotas/{id}")]
        public IActionResult TraerMisMascotas(int id)
        {
            List<Mascota> misMascotas  = new List<Mascota>();
            List<MascotaViewModel> misMascotasViewModel = new List<MascotaViewModel>();

            misMascotas = usuarioService.TraerMisMascotas(id);

            foreach (Mascota mascota in misMascotas)
            {
                misMascotasViewModel.Add(new MascotaViewModel()
                {
                    IdMascota = mascota.IdMascota,
                    IdUsuario = mascota.IdUsuario,
                    Nombre = mascota.Nombre,
                    Perdida = mascota.Perdida,
                    Avatar = mascotaService.TraerAvatarMascota(mascota.IdMascota),
                    DescripcionRaza = mascotaService.TraeDescripcionRaza(mascota.IdRaza)                    
                });
            }
            return this.Ok(misMascotasViewModel);
        }

        // POST: api/<controller>/ValidarUsuario
        [HttpPost("/api/Usuario/ValidarUsuario")]
        public IActionResult ValidarUsuario([FromBody]UsuarioViewModel rdUser)
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

        [HttpGet]
        [Route("api/Usuario/PruebaGet")]
        public IActionResult PruebaGet()
        {
            return this.Ok();
        }
    }
}