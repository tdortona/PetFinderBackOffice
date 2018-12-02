using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Repositories;
using PetFinderBackOffice.ViewModels;

namespace PetFinderBackOffice.Services
{
    public class ImagenMascotaService
    {
        private readonly ImagenMascotaRepository imagenMascotaRepository = new ImagenMascotaRepository();
        private readonly string encontradosPath = AppContext.BaseDirectory +  "Resources\\Img\\Mascotas\\Encontrados";
        private readonly string mascotasPath = AppContext.BaseDirectory + "Resources\\Img\\Mascotas\\";

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

        public void GuardarFotoEnServidor(string imagenBase64, string nombreImagen, bool encontrado){
            HttpClient client = new HttpClient();
            String uri = "http://criaderononthue.com/img/canfind/controllers/guardarImagenController.php";
            if( encontrado )
            {
                uri = "http://criaderononthue.com/img/canfind/controllers/guardarImagenEncontradoController.php";
            };

            var parametros = new ImagenAGuardar
            {
                imagen64 = imagenBase64,
                nameFile = nombreImagen
            };
                //using System.Net.Http.Headers;
            client.PostAsJsonAsync( uri, JsonConvert.SerializeObject(parametros));

        }
    }
}
