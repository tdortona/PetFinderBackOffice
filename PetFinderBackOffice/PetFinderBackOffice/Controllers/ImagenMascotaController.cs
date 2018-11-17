using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.Util;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PetFinderBackOffice.Models;

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

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            List<string> classifierIds = new List<string>
            {
                "DogsBreed_2053415849"
            };

            VisualRecognitionService visualRecognition = new VisualRecognitionService();
            visualRecognition.SetEndpoint(endpoint);
            visualRecognition.VersionDate = "2018-03-19";

            FileStream img;
            img = new FileStream("C:\\Siena\\fran.jpg", FileMode.Open);

            TokenOptions iamAssistantTokenOptions = new TokenOptions()
            {
                IamApiKey = apikey
            };

            visualRecognition.SetCredential(iamAssistantTokenOptions);
            
            var result = visualRecognition.Classify(img, classifierIds: classifierIds);

            JObject jsonResult = JObject.Parse(result.ResponseJson);

            return this.Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
