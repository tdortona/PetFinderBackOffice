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
        private readonly string encontradosPath = AppDomain.CurrentDomain.BaseDirectory +  "Resources\\Img\\Mascotas\\Encontrados";
        private readonly string mascotasPath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\Img\\Mascotas\\";

        public int AddImagenMascotaEncontrada(string path, string localizacion, int idUsuario)
        {
            return this.imagenMascotaRepository.AddImagenMascotaEncontrada(path, localizacion, idUsuario);
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
