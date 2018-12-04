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
        private readonly LogErroresService logErroresService = new LogErroresService();
        private readonly string encontradosPath = AppContext.BaseDirectory + "Resources\\Img\\Mascotas\\Encontrados";
        private readonly string mascotasPath = AppContext.BaseDirectory + "Resources\\Img\\Mascotas\\";
        private readonly string nuevasClasesPath = AppContext.BaseDirectory;
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
            try
            {
                if (!Directory.Exists(encontradosPath))
                {
                    Directory.CreateDirectory(encontradosPath);
                }

                string imageName = DateTime.Now.Ticks.ToString();

                var img = Convert.FromBase64String(imagenVM.ImageURI);
                System.IO.File.WriteAllBytes(encontradosPath + "//" + imageName + ".jpg", img);

                await this.imagenMascotaService.GuardarFotoEnServidor(imagenVM.ImageURI, imageName, true );

                int idImagen = this.imagenMascotaService.AddImagenMascotaEncontrada(imageName, imagenVM.Localizacion, imagenVM.IdUsuario);

                var res = this.EnviarFotoAWatson(encontradosPath + "//" + imageName + ".jpg", idImagen);

                return this.Ok(res);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
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
        public async Task<IActionResult> AgregarFoto([FromBody]ImageFromMascota imagenVM)
        {
            try
            {
                string imageName = DateTime.Now.Ticks.ToString();

                await this.imagenMascotaService.GuardarFotoEnServidor(imagenVM.ImageURI, imageName, false);

                this.imagenMascotaService.AddImagenMascota(imageName, imagenVM.IdMascota, imagenVM.IdUsuario);

                List<string> totalFotosMascota = this.imagenMascotaService.TraerFotosMascota(imagenVM.IdMascota);
                if( totalFotosMascota.Count() == 10 ){
                    //crearClaseWatson
                    int algo = 1;
                }

                return this.Ok();
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        [HttpPost("/api/ImagenMascota/CrearClaseWatson")]
        public async Task<IActionResult> CrearClaseWatson([FromBody]CrearClaseWatsonViewModel imagenes)
        {
            try
            {
                VisualRecognitionService visualRecognition = new VisualRecognitionService();
                visualRecognition.SetEndpoint(endpoint);
                visualRecognition.VersionDate = versionDate;
                TokenOptions iamAssistantTokenOptions = new TokenOptions()
                {
                    IamApiKey = apikey
                };
                visualRecognition.SetCredential(iamAssistantTokenOptions);

                string zipPath = await this.GetImagesByteArrays(imagenes.IdMascota, imagenes.NombreMascota, imagenes.ImagesUris);

                Dictionary<string, Stream> possitiveExamples = new Dictionary<string, Stream>();
                FileStream zipFileStream;
                zipFileStream = new FileStream(zipPath, FileMode.Open);
                possitiveExamples.Add(imagenes.IdMascota + "_" + imagenes.NombreMascota, zipFileStream);

                var result = visualRecognition.UpdateClassifier(new UpdateClassifier("DogsBreed_2053415849", possitiveExamples, null), null);

                return this.Ok();
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        private async Task<string> GetImagesByteArrays(int idMascota, string nombreMascota, List<string> imageUris)
        {
            List<byte[]> fileBytes = new List<byte[]>();
            byte[] file;

            using (var client = new HttpClient())
            {
                foreach (var item in imageUris)
                {
                    using (var asd = await client.GetAsync(item))
                    {
                        if (asd.IsSuccessStatusCode)
                        {
                            file = await asd.Content.ReadAsByteArrayAsync();
                            fileBytes.Add(file);
                        }
                    }
                }
            }

            return this.WriteImagesBytes(idMascota, nombreMascota, fileBytes);
        }

        private string WriteImagesBytes(int idMascota, string nombreMascota, List<byte[]> fileBytes)
        {
            int i = 0;
            string path = nuevasClasesPath + "//" + idMascota.ToString() + " - " + nombreMascota;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var item in fileBytes)
            {
                System.IO.File.WriteAllBytes(path + "//" + i.ToString() + ".jpg", item);
                i++;
            }

            return this.GetZipFromFiles(path, idMascota, nombreMascota);
        }

        private string GetZipFromFiles(string path, int idMascota, string nombreMascota)
        {
            try
            {
                string newFolderPath = nuevasClasesPath + "//" + idMascota.ToString() + " - " + nombreMascota + " - ArchivoZip";
                if (!Directory.Exists(newFolderPath))
                {
                    Directory.CreateDirectory(newFolderPath);
                }

                string filePath = newFolderPath + "//" + nombreMascota + "_positive_examples.zip";
                ZipFile.CreateFromDirectory(path, filePath);
                return filePath;
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }

        private void GuardarInteraccionConWatson(ClassifiedImages result, int idImagen)
        {
            this.consultasWatsonService.GuardarInteraccionConWatson(result, idImagen);
        }

        // GET api/<controller>
        [HttpGet("/api/ImagenMascota/TraerFotos/{idMascota}"), DisableRequestSizeLimit]
        public IActionResult TraerFotos(int idMascota)
        {
            try
            {
                List<string> fotosArray = this.imagenMascotaService.TraerFotosMascota(idMascota);

                return this.Ok(fotosArray);
            }
            catch (Exception e)
            {
                this.logErroresService.LogError(e.Message + " " + e.InnerException + " " + e.TargetSite + " " + this.GetType().ToString().Split('.')[2]);
                throw;
            }
        }
    }
}
