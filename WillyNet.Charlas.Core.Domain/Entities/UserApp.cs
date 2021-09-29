using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class UserApp : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Dni { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; }
    }
}
