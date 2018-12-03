using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Util;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using IBM.WatsonDeveloperCloud.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Services;
using PetFinderBackOffice.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinderBackOffice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class ImagenMascotaController : Controller
    {
        private readonly ConsultasWatsonService consultasWatsonService = new ConsultasWatsonService();
        private readonly ImagenMascotaService imagenMascotaService = new ImagenMascotaService();
        private readonly string encontradosPath = AppContext.BaseDirectory + "Resources\\Img\\Mascotas\\Encontrados";
        private readonly string mascotasPath = AppContext.BaseDirectory + "Resources\\Img\\Mascotas\\";
        private const string versionDate = "2018-03-19";
        private const string apikey = "HY_-KRN409tGl3X4Yp3zrbVxKpLGugfZ5HPr2gsCGMiC";
        private const string endpoint = "https://gateway.watsonplatform.net/visual-recognition/api";
        private static readonly List<string> classifierIds = new List<string>
        {
            "DogsBreed_2053415849"
        };

        // POST api/<controller>
        [HttpPost("/api/ImagenMascota/FotoEncontrado"), DisableRequestSizeLimit]
        public async Task<IActionResult> FotoEncontrado([FromBody]ImageFromServerModel imagenVM)
        {
            if (!Directory.Exists(encontradosPath))
            {
                Directory.CreateDirectory(encontradosPath);
            }

            string imageName = DateTime.Now.Ticks.ToString();

            var img = Convert.FromBase64String(imagenVM.ImageURI);
            System.IO.File.WriteAllBytes(encontradosPath + "//" + imageName + ".jpg", img);

            await this.imagenMascotaService.GuardarFotoEnServidor(imagenVM.ImageURI, imageName, true );

            imagenVM.Localizacion = "LOCALIZACION";
            int idImagen = this.imagenMascotaService.AddImagenMascotaEncontrada(imageName, imagenVM.Localizacion, imagenVM.IdUsuario);

            var res = this.EnviarFotoAWatson(encontradosPath + "//" + imageName + ".jpg", idImagen);

            return this.Ok(res);
        }
        
        private string EnviarFotoAWatson(string path, int idImagen)
        {
            VisualRecognitionService visualRecognition = new VisualRecognitionService();
            visualRecognition.SetEndpoint(endpoint);
            visualRecognition.VersionDate = versionDate;

            FileStream img;
            img = new FileStream(path, FileMode.Open);

            TokenOptions iamAssistantTokenOptions = new TokenOptions()
            {
                IamApiKey = apikey
            };

            visualRecognition.SetCredential(iamAssistantTokenOptions);
            
            var result = visualRecognition.Classify(img, classifierIds: classifierIds);

            this.GuardarInteraccionConWatson(result, idImagen);

            img.Close();

            return result.ResponseJson;
        }

        // POST api/<controller>
        [HttpPost("/api/ImagenMascota/AgregarFoto"), DisableRequestSizeLimit]
        public void AgregarFoto([FromBody]ImageFromServerModel imagenVM)
        {
            //Resources//Img//Mascotas//{idMascota}//...jpg
            if (!Directory.Exists("Resources//ImagenesMascota"))
            {
                Directory.CreateDirectory("Resources//ImagenesMascota");
            }

            long ahora = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            String nombreImagen = ahora.ToString(); 
            
            var img = Convert.FromBase64String(imagenVM.ImageURI);
            System.IO.File.WriteAllBytes("Resources//ImagenesMascota//" + nombreImagen + ".jpg", img);
        }

        [HttpGet("/api/ImagenMascota/CrearClaseWatson")]
        public IActionResult CrearClaseWatson()
        {
            VisualRecognitionService visualRecognition = new VisualRecognitionService();
            
            visualRecognition.SetEndpoint(endpoint);
            visualRecognition.VersionDate = versionDate;
            TokenOptions iamAssistantTokenOptions = new TokenOptions()
            {
                IamApiKey = apikey
            };

            visualRecognition.SetCredential(iamAssistantTokenOptions);
            Dictionary<string, Stream> possitiveExamples = new Dictionary<string, Stream>();
            FileStream img;
            img = new FileStream("C:\\CanFind\\francisco2_positive_examples.zip", FileMode.Open);
            possitiveExamples.Add("francisco2_positive_examples.zip", img);

            var result = visualRecognition.UpdateClassifier(new UpdateClassifier("DogsBreed_2053415849", possitiveExamples, null), null);

            return this.Ok();
        }

        private void GuardarInteraccionConWatson(ClassifiedImages result, int idImagen)
        {
            this.consultasWatsonService.GuardarInteraccionConWatson(result, idImagen);
        }
    }
}
