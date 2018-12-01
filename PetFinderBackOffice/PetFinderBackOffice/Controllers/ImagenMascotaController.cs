using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Util;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetFinderBackOffice.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class ImagenMascotaController : Controller
    {
        private const string versionDate = "2018-03-19";
        private const string apikey = "HY_-KRN409tGl3X4Yp3zrbVxKpLGugfZ5HPr2gsCGMiC";
        private const string endpoint = "https://gateway.watsonplatform.net/visual-recognition/api";
        private static readonly List<string> classifierIds = new List<string>
        {
            "DogsBreed_2053415849"
        };

        // POST api/<controller>
        [HttpPost("/api/ImagenMascota/FotoEncontrado"), DisableRequestSizeLimit]
        public IActionResult FotoEncontrado([FromBody]ImageFromServerModel imagenVM)
        {
            if (!Directory.Exists("ImagenesMascota"))
            {
                Directory.CreateDirectory("ImagenesMascota");
            }

            var img = Convert.FromBase64String(imagenVM.ImageURI);
            System.IO.File.WriteAllBytes("ImagenesMascota//prueba.jpg", img);

            var res = this.EnviarFotoAWatson("ImagenesMascota//prueba.jpg");

            return this.Ok(res);
        }

        private string EnviarFotoAWatson(string path)
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
    }
}
