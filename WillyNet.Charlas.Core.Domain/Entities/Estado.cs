using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Common;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class Estado : AuditableBaseEntity
    {
        public Guid EstadoId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Evento> Eventos { get; set; }
    }
}
