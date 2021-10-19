using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Common;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class Evento : AuditableBaseEntity
    {
        private DateTime _fechaFin;
        public Guid EventoId { get; set; }
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }
        public bool DeleteLog { get; set; }
        public DateTime FechaFin {
            get { 
                _fechaFin = FechaIni.AddHours(Duracion);
                return _fechaFin;        
            }
            set { _fechaFin = value; }
        }
        public Guid CharlaId { get; set; }
        public Charla Charla { get; set; }
        public Guid EstadoEventoId { get; set; }
        public EstadoEvento EstadoEvento { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; }
    }
}
