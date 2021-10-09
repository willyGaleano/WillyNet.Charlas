using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Charlas
{
    public class GetAllCharlaSpecification : Specification<Charla>
    {
        public GetAllCharlaSpecification(int pageNumber, int pageSize, string nombre)
        {
            Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            Query.Search(x => x.Nombre, "%" + nombre + "%");
                
        }
    }
}
