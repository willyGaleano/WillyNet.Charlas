using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Common;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class Asistencia : AuditableBaseEntity
    {
        public Guid AsistenciaId { get; set; }
        public bool Asistio { get; set; }
        public Guid CharlaEventoId { get; set; }
        public string UserAppId { get; set; }
        public CharlaEvento CharlaEvento { get; set; }
        public UserApp UserApp { get; set; }
    }
}
