using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Asistencias
{
    public class CountCantEventAsistSpecification : Specification<Asistencia>
    {
        public CountCantEventAsistSpecification(Guid id)
        {
            Query.Where(x => x.EventoId == id);
        }
    }
}
