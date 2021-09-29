using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class CharlaEventoDto
    {
        public Guid CharlaEventoId { get; set; }
        public CharlaDto Charla { get; set; }
        public EventoDto Evento { get; set; }
        
    }
}
