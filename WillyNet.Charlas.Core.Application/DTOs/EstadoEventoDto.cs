using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class EstadoEventoDto
    {
        public Guid EstadoEventoId { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
    }
}
