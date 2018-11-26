using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Repositories;
using PetFinderBackOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();

        public Usuario BuscaUsuarioPorId(string id)
        {
            return usuarioRepository.BuscaUsuarioPorId(id);
        }

        public void RegistrarUsuario(UsuarioViewModel usuarioNuevo)
        {
            usuarioRepository.RegistrarUsuario(usuarioNuevo);
        }

        public List<Mascota> TraerMisMascotas(int idUsuario)
        {
            return usuarioRepository.TraerMisMascotas(idUsuario);
        }
    }
}
