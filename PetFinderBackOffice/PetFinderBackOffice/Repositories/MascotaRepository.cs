using PetFinderBackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.Repositories
{
    public class MascotaRepository
    {
        private readonly PetFinderDBContext context = new PetFinderDBContext();

        public string TraeDescripcionRaza(int idRaza)
        {
            string descripcion = this.context.Raza
                .Where(r => r.IdRaza == idRaza)
                .Select(d => d.Descripcion).FirstOrDefault();
                ;
            return descripcion;
        }
    }
}
