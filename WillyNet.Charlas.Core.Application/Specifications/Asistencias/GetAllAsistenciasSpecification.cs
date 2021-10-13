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
        public GetAllAsistenciasSpecification(int pageNumber, int pageSize, string nombre, string userAppId)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize);
            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Evento.Charla.Nombre, "%" +nombre + "%");

            if (!string.IsNullOrEmpty(userAppId))
                Query.Where(x => x.UserAppId == userAppId);
            Query
                .Include(x => x.EstadoAsistencia)
                .Include(x => x.UserApp)
                .Include(x => x.Evento)
                .ThenInclude(x => x.Charla);

            Query
                .Include(x => x.Evento)
                .ThenInclude(x => x.EstadoEvento);                
        }
    }
}
