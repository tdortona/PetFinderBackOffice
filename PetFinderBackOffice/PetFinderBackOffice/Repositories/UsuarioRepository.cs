using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.Repositories
{
    public class UsuarioRepository
    {
        private readonly PetFinderDBContext context = new PetFinderDBContext();

        public Usuario BuscaUsuarioPorId(string id)
        {
            Usuario usuario = this.context.Usuario.LastOrDefault(u => u.IdUsuarioRedSocial == id);
            return usuario;
        }

        public void RegistrarUsuario(UsuarioViewModel usuarioNuevo)
        {
            Usuario usuario = new Usuario
            {
                Nombre = usuarioNuevo.Name,
                Email = usuarioNuevo.Email,
                Avatar = usuarioNuevo.Avatar,
                IdUsuarioRedSocial = usuarioNuevo.Id,
                IdRedSocial = usuarioNuevo.IdRedSocial,
                TelefonoContacto = "llamameaca"
            };

            this.context.Usuario.Add(usuario);

            this.context.SaveChanges();
        }
    }
}
