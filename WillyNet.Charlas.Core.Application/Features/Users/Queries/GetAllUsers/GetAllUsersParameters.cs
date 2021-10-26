using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Core.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersParameters : RequestParameter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Dni { get; set; }
    }
}
