using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.CharlasEventos
{
    public class GetAllPagedChalasEventosSpecification : Specification<CharlaEvento>
    {
        public GetAllPagedChalasEventosSpecification(int pageNumber, int pageSize, string nombre)
        {
            Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            Query.Search(x => x.Charla.Nombre, "%" + nombre + "%");
            Query
                .Include(x => x.Charla)
                .Include(x => x.Evento).ThenInclude(x => x.Estado);

        }
    }
}
