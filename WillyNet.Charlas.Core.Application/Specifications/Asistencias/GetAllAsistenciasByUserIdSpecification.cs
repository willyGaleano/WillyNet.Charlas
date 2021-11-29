using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Asistencias
{
    public class GetAllAsistenciasByUserIdSpecification : Specification<Asistencia>
    {
        public GetAllAsistenciasByUserIdSpecification(string userAppId)
        {
            Query.Where(x => x.UserAppId == userAppId);
            Query.Include(x => x.EstadoAsistencia);
        }
    }
}
