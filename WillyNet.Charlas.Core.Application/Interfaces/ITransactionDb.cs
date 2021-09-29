﻿using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.Interfaces
{
    public interface ITransactionDb
    {
        IDbContextTransaction DbContextTransaction { get; }
    }
}
