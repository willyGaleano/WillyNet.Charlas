using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Common;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class EstadoAsistencia : AuditableBaseEntity
    {
        public Guid EstadoAsistenciaId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; }
    }
}
