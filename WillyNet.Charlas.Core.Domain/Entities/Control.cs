using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Common;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class Control : AuditableBaseEntity
    {
        public Guid ControlId { get; set; }
        public DateTime FecSesion { get; set; }
        public short Tope { get; set; }
        public string UserAppId { get; set; }
        public UserApp UserApp { get; set; }
    }
}
