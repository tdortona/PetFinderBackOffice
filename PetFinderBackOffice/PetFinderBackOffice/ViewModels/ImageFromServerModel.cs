﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinderBackOffice.ViewModels
{
    public class ImageFromServerModel
    {
        public int IdUsuario { get; set; }

        public string ImageURI { get; set; }

        public string Localizacion { get; set; }
    }
}
