using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetFinderBackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.Repositories
{
    public class UsuarioRepository
    {
        private readonly PetFinderDBContext context = new PetFinderDBContext();

        public Usuario BuscaUsuarioPorId(int id)
        {
            Usuario usuario = this.context.Usuario.LastOrDefault(u => u.IdRedSocial == id);
            return usuario;
        }
    }
}
