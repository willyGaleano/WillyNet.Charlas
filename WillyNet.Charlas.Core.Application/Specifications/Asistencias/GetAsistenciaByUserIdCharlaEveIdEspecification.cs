using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Asistencias
{
    public class GetAsistenciaByUserIdCharlaEveIdEspecification : Specification<Asistencia>, ISingleResultSpecification
    {
        public GetAsistenciaByUserIdCharlaEveIdEspecification(string userAppId, Guid charlaEventoId)
        {
            Query.Where(x => x.UserAppId == userAppId && x.CharlaEventoId == charlaEventoId);
        }
    }
}
