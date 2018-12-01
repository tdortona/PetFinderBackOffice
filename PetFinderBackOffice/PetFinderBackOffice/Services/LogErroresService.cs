using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetFinderBackOffice.Models;

namespace PetFinderBackOffice.Services
{
    public class LogErroresService
    {
        private readonly PetFinderDBContext context = new PetFinderDBContext();

        public void LogError(string msg)
        {
            LogError err = new LogError
            {
                MensajeExcepcion = msg
            };

            this.context.LogError.Add(err);

            this.context.SaveChanges();
        }
    }
}
