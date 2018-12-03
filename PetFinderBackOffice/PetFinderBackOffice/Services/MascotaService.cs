using PetFinderBackOffice.Models;
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

        public string TraerAvatarMascota(int idMascota)
        {
            return mascotaRepository.TraerAvatarMascota(idMascota);
        }

        public Mascota TraerMascota(int idMascota)
        {
            return mascotaRepository.TraerMascota(idMascota);
        }

        public void ReportarPerdida(int IdMascota)
        {
            mascotaRepository.ReportarPerdida(IdMascota);
        }

        public void ReportarEncontrada(int IdMascota)
        {
            mascotaRepository.ReportarEncontrada(IdMascota);
        }

        public void AgregarMascotaNueva(Mascota mascota)
        {
            mascotaRepository.AgregarMascotaNueva(mascota);
        }

        public List<Raza> TraerRazas()
        {
            return mascotaRepository.TraerRazas();
        }
    }
}
