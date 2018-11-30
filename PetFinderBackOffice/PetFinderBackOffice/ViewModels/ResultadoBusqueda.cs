using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.ViewModels
{
    public class ResultadoBusqueda
    {
        public int IdUsuario { get; set; }

        public string Clase { get; set; }

        public float Score { get; set; }

        public string Imagen { get; set; }
    }
}
