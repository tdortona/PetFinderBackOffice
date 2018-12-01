using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using PetFinderBackOffice.Models;
using PetFinderBackOffice.Repositories;

namespace PetFinderBackOffice.Services
{
    public class ConsultasWatsonService
    {
        private readonly ConsultasWatsonRepository consultasWatsonRepository = new ConsultasWatsonRepository();

        public void GuardarInteraccionConWatson(ClassifiedImages result, int idImagen)
        {
            this.consultasWatsonRepository.GuardarInteraccionConWatson(result, idImagen);
        }

        public List<ConsultasWatson> ConsultarEncontrados(string claseNombre, string claseRaza, int score)
        {
            return this.consultasWatsonRepository.ConsultarEncontrados(claseNombre, claseRaza, score);
        }
    }
}
