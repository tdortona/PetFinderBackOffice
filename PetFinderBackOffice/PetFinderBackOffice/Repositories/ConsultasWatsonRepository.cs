﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.VisualRecognition.v3.Model;
using PetFinderBackOffice.Models;

namespace PetFinderBackOffice.Repositories
{
    public class ConsultasWatsonRepository
    {
        private readonly PetFinderDBContext context = new PetFinderDBContext();

        public void GuardarInteraccionConWatson(ClassifiedImages result, int idImagen)
        {
            ConsultasWatson consulta = new ConsultasWatson
            {
                Clase = result.Images.FirstOrDefault()?.Classifiers.FirstOrDefault()?.Classes.FirstOrDefault()?.ClassName,
                Score = result.Images.FirstOrDefault()?.Classifiers.FirstOrDefault()?.Classes.FirstOrDefault()?.Score.Value * 100,
                IdImagen = idImagen
            };

            this.context.ConsultasWatson.Add(consulta);

            this.context.SaveChanges();
        }

        public List<ConsultasWatson> ConsultarEncontrados(string claseNombre, string claseRaza, int score)
        {
            var consultas = this.context.ConsultasWatson.ToList();
            List<ConsultasWatson> consultasNombre = new List<ConsultasWatson>();
            List<ConsultasWatson> consultasRaza = new List<ConsultasWatson>();

            if (!string.IsNullOrWhiteSpace(claseNombre))
            {
                consultasNombre = consultas.Where(x => !string.IsNullOrWhiteSpace(x.Clase) && x.Clase.ToLower().Contains(claseNombre.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(claseRaza))
            {
                consultasRaza = consultas.Where(x => !string.IsNullOrWhiteSpace(x.Clase) && x.Clase.ToLower().Contains(claseRaza.ToLower())).ToList();
            }

            if (consultasNombre.Count > 0 || consultasRaza.Count > 0)
            {
                consultas = consultasNombre.Concat(consultasRaza).Distinct().ToList();
            }
            else if (!string.IsNullOrWhiteSpace(claseNombre) || !string.IsNullOrWhiteSpace(claseRaza))
            {
                consultas = new List<ConsultasWatson>();
            }

            return consultas.Where(x => x.Score.HasValue && x.Score >= score).ToList();
        }
    }
}
