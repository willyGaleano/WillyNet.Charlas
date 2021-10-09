using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.Parameters
{
    public class CharlaRequest
    {
        public string NombreCharla { get; set; }
        public string DescripcionCharla { get; set; }
        public IFormFile File { get; set; }
    }
}
