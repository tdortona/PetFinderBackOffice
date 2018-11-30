using System;
using System.Collections.Generic;

namespace PetFinderBackOffice.Models
{
    public partial class ConsultasWatson
    {
        public int IdConsulta { get; set; }
        public string Clase { get; set; }
        public float? Score { get; set; }
        public int IdImagen { get; set; }

        public ImagenMascota IdImagenNavigation { get; set; }
    }
}
