using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class EventoDto
    {        
        public Guid EventoId { get; set; }
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoDto Estado { get; set; }
        
    }
}
