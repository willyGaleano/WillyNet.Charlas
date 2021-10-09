using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Controls
{
    public class CountControlByUserDateSpecification : Specification<Control>
    {
        public CountControlByUserDateSpecification(string userId, DateTime fechaRegistro)
        {
            Query.Where(x => x.UserAppId == userId && x.FecSesion == fechaRegistro);
        }
    }
}
