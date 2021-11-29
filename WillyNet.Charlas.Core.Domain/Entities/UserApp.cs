using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WillyNet.Charlas.Core.Domain.Entities
{
    public class UserApp : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Dni { get; set; }
        public bool DeleteLog { get; set; }
        public string ImgUrl { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<Asistencia> Asistencias { get; set; }
        public ICollection<Control> Controls { get; set; }        
    }
}
