using PetFinderBackOffice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.Services
{
    public class MascotaService
    {
        private readonly MascotaRepository mascotaRepository = new MascotaRepository(); 

        public string TraeDescripcionRaza(int idRaza)
        {
            return mascotaRepository.TraeDescripcionRaza(idRaza);
        }
    }
}
