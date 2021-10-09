using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Core.Application.Features.CharlasEventos.Queries.GetAllCharlasEventos
{
    public class GetAllCharlasEventosParameters : RequestParameter
    {
        public string Nombre { get; set; }
    }
}
