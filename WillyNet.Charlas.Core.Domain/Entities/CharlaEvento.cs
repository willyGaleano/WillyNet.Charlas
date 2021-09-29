using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Common;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class CharlaEvento : AuditableBaseEntity
    {
        public Guid CharlaEventoId { get; set; }
        public Guid CharlaId { get; set; }
        public Guid EventoId { get; set; }
        public Charla Charla { get; set; }
        public Evento Evento { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; }
    }
}
