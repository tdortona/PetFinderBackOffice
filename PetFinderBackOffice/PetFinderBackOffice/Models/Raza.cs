using System;
using System.Collections.Generic;

namespace PetFinderBackOffice.Models
{
    public partial class Raza
    {
        public Raza()
        {
            Mascota = new HashSet<Mascota>();
        }

        public int IdRaza { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Mascota> Mascota { get; set; }
    }
}
