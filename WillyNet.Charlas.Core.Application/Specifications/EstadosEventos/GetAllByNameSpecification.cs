using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.EstadosEventos
{
    public class GetAllByNameSpecification : Specification<EstadoEvento>
    {
        public GetAllByNameSpecification(string nombre, int pageNumber, int pageSize)
        {
            Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            Query.Where(x => x.DeleteLog == false);
        }
    }
}
