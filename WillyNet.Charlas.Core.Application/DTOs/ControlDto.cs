using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.DTOs
{
    public class ControlDto
    {
        public Guid ControlId { get; set; }
        public DateTime FecSesion { get; set; }
        public short Tope { get; set; }

        public string UserAppId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }
    }
}
