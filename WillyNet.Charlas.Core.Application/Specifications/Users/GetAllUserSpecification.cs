using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Users
{
    public class GetAllUserSpecification : Specification<UserApp>
    {
        public GetAllUserSpecification(string firstName, string lastName, int dni,int pageNumber, int pageSize)
        {
            Query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(firstName))
                Query.Search(x => x.FirstName, "%" + firstName + "%");

            if (!string.IsNullOrEmpty(lastName))
                Query.Search(x => x.LastName, "%" +lastName + "%");
            if(dni != 0)
                Query.Where(x => x.Dni == dni);

            Query.Where(x => x.DeleteLog == false);
        }
    }
}
