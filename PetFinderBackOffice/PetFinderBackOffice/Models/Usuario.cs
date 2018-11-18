using System;
using System.Collections.Generic;

namespace PetFinderBackOffice.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            ImagenMascota = new HashSet<ImagenMascota>();
            Mascota = new HashSet<Mascota>();
        }

        public int IdUsuario { get; set; }
        public string Avatar { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public int IdRedSocial { get; set; }
        public string IdUsuarioRedSocial { get; set; }
        public string TelefonoContacto { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
        public Usuario InverseIdUsuarioNavigation { get; set; }
        public ICollection<ImagenMascota> ImagenMascota { get; set; }
        public ICollection<Mascota> Mascota { get; set; }
    }
}
