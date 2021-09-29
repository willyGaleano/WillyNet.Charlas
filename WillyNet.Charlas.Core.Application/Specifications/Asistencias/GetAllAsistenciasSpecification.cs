using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Asistencias
{
    public class GetAllAsistenciasSpecification : Specification<Asistencia>
    {
        public GetAllAsistenciasSpecification(int pageNumber, int pageSize)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize);                            

            Query
                .Include(x => x.UserApp)
                .Include(x => x.CharlaEvento)
                .ThenInclude(x => x.Charla);

            Query
                .Include(x => x.CharlaEvento)
                .ThenInclude(x => x.Evento)
                .ThenInclude(x => x.Estado);
        }
    }
}
