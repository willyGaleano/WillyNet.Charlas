using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Charlas
{
    public class GetAllCharlasActivaSpecification : Specification<Charla>
    {
        public GetAllCharlasActivaSpecification()
        {
            Query.Where(x => x.DeleteLog == false);
        }
    }
}
