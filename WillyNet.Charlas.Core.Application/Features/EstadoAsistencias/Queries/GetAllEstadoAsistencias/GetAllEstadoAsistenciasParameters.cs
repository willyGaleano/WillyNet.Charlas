using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Core.Application.Features.EstadoAsistencias.Queries.GetAllEstadoAsistencias
{
    public class GetAllEstadoAsistenciasParameters : RequestParameter
    {
        public string Nombre { get; set; }
    }
}
