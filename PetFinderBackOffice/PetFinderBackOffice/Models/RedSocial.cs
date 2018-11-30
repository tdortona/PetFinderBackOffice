using System;
using System.Collections.Generic;

namespace PetFinderBackOffice.Models
{
    public partial class RedSocial
    {
        public RedSocial()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdRedSocial { get; set; }
        public string Nombre { get; set; }

        public ICollection<Usuario> Usuario { get; set; }
    }
}
