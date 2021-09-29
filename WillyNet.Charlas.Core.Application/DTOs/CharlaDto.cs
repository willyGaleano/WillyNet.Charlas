using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class CharlaDto
    {
        public Guid CharlaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImage { get; set; }        
    }
}
