using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Infraestructure.Persistence.Contexts;

namespace WillyNet.Charlas.Infraestructure.Persistence.Services
{
    public class TransactionDb : ITransactionDb
    {
        public IDbContextTransaction DbContextTransaction { get; }
        public TransactionDb(DbCharlaContext context)
        {
            DbContextTransaction = context.Database.BeginTransaction();
        }
    }
}
