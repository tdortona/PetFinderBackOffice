using System;
using System.Collections.Generic;

namespace PetFinderBackOffice.Models
{
    public partial class Mascota
    {
        public Mascota()
        {
            ImagenMascota = new HashSet<ImagenMascota>();
        }

        public int IdMascota { get; set; }
        public int IdUsuario { get; set; }
        public int IdRaza { get; set; }
        public string Nombre { get; set; }
        public bool Perdida { get; set; }
        public bool Entrenada { get; set; }

        public Raza IdRazaNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<ImagenMascota> ImagenMascota { get; set; }
    }
}
