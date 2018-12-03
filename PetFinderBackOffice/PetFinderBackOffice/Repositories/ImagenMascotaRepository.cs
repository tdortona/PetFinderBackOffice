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
                IdMascota = null,
                IdUsuario = idUsuario,
                ImagenPath = path,
                Localizacion = localizacion
            };

            this.context.ImagenMascota.Add(imagenMascota);

            this.context.SaveChanges();

            return imagenMascota.IdImagen;
        }

        public void AddImagenMascota(string path, int idMascota, int idUsuario)
        {
            ImagenMascota imagenMascota = new ImagenMascota
            {
                IdMascota = idMascota,
                IdUsuario = idUsuario,
                ImagenPath = path,
                Localizacion = string.Empty
            };

            this.context.ImagenMascota.Add(imagenMascota);

            this.context.SaveChanges();
        }

        public ImagenMascota GetImagenMascota(int id)
        {
            return this.context.ImagenMascota.FirstOrDefault(x => x.IdImagen == id);
        }

        public List<string> ListarFotos(int id)
        {
            var fotos =
                from imagenes in this.context.ImagenMascota
                where imagenes.IdMascota == id
                select imagenes.ImagenPath;

            return fotos.ToList();
        }
    }
}
