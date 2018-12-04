using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.ViewModels
{
    public class CrearClaseWatsonViewModel
    {
        public int IdMascota { get; set; }

        public string NombreMascota { get; set; }

        public List<string> ImagesUris { get; set; }
    }
}
