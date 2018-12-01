using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetFinderBackOffice.Models;

namespace PetFinderBackOffice.Repositories
{
    public class ImagenMascotaRepository
    {
        private readonly PetFinderDBContext context = new PetFinderDBContext();

        public int AddImagenMascotaEncontrada(string path, string localizacion, int idUsuario)
        {
            ImagenMascota imagenMascota = new ImagenMascota
            {
                IdMascota = 0,
                IdUsuario = idUsuario,
                ImagenPath = path,
                Localizacion = localizacion
            };

            this.context.ImagenMascota.Add(imagenMascota);

            this.context.SaveChanges();

            return imagenMascota.IdImagen;
        }

        public ImagenMascota GetImagenMascota(int id)
        {
            return this.context.ImagenMascota.FirstOrDefault(x => x.IdImagen == id);
        }
    }
}
