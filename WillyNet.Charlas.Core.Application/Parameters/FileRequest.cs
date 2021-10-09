﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Helpers;

namespace WillyNet.Charlas.Core.Application.Parameters
{
    public class FileRequest
    {
        public Stream Content { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        
        public string GetPathWithFileName(Guid id)
        {
            string ext = Path.GetExtension(Name);
            string basePath = "appcharlas/charlas/";
            return basePath + id + ext;
        }

        


    }
}