using System;
using System.Collections.Generic;

namespace PetFinderBackOffice.Models
{
    public partial class ImagenMascota
    {
        public ImagenMascota()
        {
            ConsultasWatson = new HashSet<ConsultasWatson>();
        }

        public int IdImagen { get; set; }
        public int? IdMascota { get; set; }
        public string Localizacion { get; set; }
        public string ImagenPath { get; set; }
        public int IdUsuario { get; set; }

        public Mascota IdMascotaNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<ConsultasWatson> ConsultasWatson { get; set; }
    }
}
