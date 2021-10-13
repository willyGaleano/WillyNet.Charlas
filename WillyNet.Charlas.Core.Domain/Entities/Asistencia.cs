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
        public Guid EventoId { get; set; }
        public Guid EstadoAsistenciaId { get; set; }
        public string UserAppId { get; set; }
        public Evento Evento { get; set; }
        public EstadoAsistencia EstadoAsistencia { get; set; }
        public UserApp UserApp { get; set; }
    }
}
