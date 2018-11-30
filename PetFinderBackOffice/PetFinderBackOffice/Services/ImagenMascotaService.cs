using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Repositories;

namespace PetFinderBackOffice.Services
{
    public class ImagenMascotaService
    {
        private readonly ImagenMascotaRepository imagenMascotaRepository = new ImagenMascotaRepository();
        private const string encontradosPath = "Resources//Img//Mascotas//Encontrados";
        private const string mascotasPath = "Resources//Img//Mascotas//";

        public int AddImagenMascotaEncontrada(string path, string localizacion)
        {
            return this.imagenMascotaRepository.AddImagenMascotaEncontrada(path, localizacion);
        }

        public ImagenMascota GetImagenMascota(int id)
        {
            return this.imagenMascotaRepository.GetImagenMascota(id);
        }

        public string GetImagen(int idMascota, string nombreFoto)
        {
            var physicalPath = idMascota != 0 ?
                mascotasPath + idMascota + "//" + nombreFoto + ".jpg" :
                encontradosPath + "//" + nombreFoto + ".jpg";

            var imgB = System.IO.File.ReadAllBytes(physicalPath);

            var img = Convert.ToBase64String(imgB);

            return img;
        }
    }
}
