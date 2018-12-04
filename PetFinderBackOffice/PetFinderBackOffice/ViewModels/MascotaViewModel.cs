using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.ViewModels
{
    public class MascotaViewModel
    {
        public int IdMascota { get; set; }
        
        public int IdUsuario { get; set; }
        
        public int IdRaza { get; set; }

        public string DescripcionRaza { get; set; }

        public string Nombre { get; set; }

        public bool Perdida { get; set; }

        public string Avatar { get; set; }

        public int Entrenado  { get; set; }
    }
}
