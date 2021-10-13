using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Common;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class Charla : AuditableBaseEntity
    {
        public Guid CharlaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImage { get; set; }
        public bool DeleteLog { get; set; }
        public ICollection<Evento> Eventos { get; set; }

    }
}
