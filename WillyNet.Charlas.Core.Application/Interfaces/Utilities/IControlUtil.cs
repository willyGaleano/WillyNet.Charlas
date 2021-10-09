using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WillyNet.Charlas.Core.Application.Interfaces.Utilities
{
    public interface IControlUtil
    {
        Task<bool> CreateOrUpdateCantControl(string userId, DateTime fechaRegistro);
    }
}
