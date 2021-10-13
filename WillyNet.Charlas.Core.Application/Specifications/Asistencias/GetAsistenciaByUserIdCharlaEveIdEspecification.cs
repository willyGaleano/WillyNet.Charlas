using Ardalis.Specification;
using System;
using System.Linq;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Specifications.Asistencias
{
    public class GetAsistenciaByUserIdCharlaEveIdEspecification : Specification<Asistencia>, ISingleResultSpecification
    {
        public GetAsistenciaByUserIdCharlaEveIdEspecification(string userAppId, Guid eventoId)
        {
            Query.Where(x => x.UserAppId == userAppId && x.EventoId == eventoId);
        }
    }
}
