using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.RefreshTokens
{
    public class GetByTokenRefreshSpecification : Specification<RefreshToken>, ISingleResultSpecification
    {
        public GetByTokenRefreshSpecification(string tokenRefresh)
        {
            Query.Where(x => x.Token == tokenRefresh);
        }
    }
}
