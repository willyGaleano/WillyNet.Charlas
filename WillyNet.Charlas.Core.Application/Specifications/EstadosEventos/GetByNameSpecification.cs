using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.EstadosEventos
{
    public class GetByNameSpecification : Specification<EstadoEvento>, ISingleResultSpecification
    {
        public GetByNameSpecification(string nombre)
        {
            Query.Where(x => x.Nombre.ToUpper() == nombre.ToUpper());
        }
    }
}
