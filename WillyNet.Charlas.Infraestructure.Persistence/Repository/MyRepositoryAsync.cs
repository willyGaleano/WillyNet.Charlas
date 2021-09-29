using Ardalis.Specification.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Infraestructure.Persistence.Contexts;

namespace WillyNet.Charlas.Infraestructure.Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly DbCharlaContext _dbCharlaContext;
        public MyRepositoryAsync(DbCharlaContext dbCharlaContext) : base(dbCharlaContext)
        {
            _dbCharlaContext = dbCharlaContext;
        }
    }
}
