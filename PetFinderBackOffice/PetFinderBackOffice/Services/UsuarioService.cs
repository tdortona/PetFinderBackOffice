using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();

        public Usuario BuscaUsuarioPorId(int id)
        {
            return usuarioRepository.BuscaUsuarioPorId(id);
        }
    }
}
