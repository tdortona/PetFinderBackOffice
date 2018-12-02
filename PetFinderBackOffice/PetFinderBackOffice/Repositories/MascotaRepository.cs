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
            return descripcion;
        }

        public string TraerAvatarMascota(int idMascota)
        {
            string imagenPath = this.context.ImagenMascota
                .Where(i => i.IdMascota == idMascota)
                .Select(p => p.ImagenPath).FirstOrDefault();
            return imagenPath;
        }

        public Mascota TraerMascota(int idMascota)
        {
            return this.context.Mascota
                .FirstOrDefault(m => m.IdMascota == idMascota);
        }

        public void ReportarPerdida(int idMascota)
        {
            Mascota mascotaBuscada = this.context.Mascota.FirstOrDefault(m => m.IdMascota == idMascota);
            mascotaBuscada.Perdida = true;
            this.context.SaveChanges();
        }

        public void ReportarEncontrada(int idMascota)
        {
            Mascota mascotaBuscada = this.context.Mascota.FirstOrDefault(m => m.IdMascota == idMascota);
            mascotaBuscada.Perdida = false;
            this.context.SaveChanges();
        }

        public void AgregarMascotaNueva(Mascota mascota)
        {
            mascota.Perdida = false;
            this.context.Mascota.Add(mascota);
            this.context.SaveChanges();
        }
    }
}
