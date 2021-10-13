using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Core.Application.Features.Asistencias.Queries.GetAllAsistencias
{
    public class GetAllAsistenciasParameters : RequestParameter
    {
        public string Nombre { get; set; }
        public string AppUserId { get; set; }
    }
}
