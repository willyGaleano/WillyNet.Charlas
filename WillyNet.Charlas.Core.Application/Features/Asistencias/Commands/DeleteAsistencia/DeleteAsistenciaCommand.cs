using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.Asistencias;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.DeleteAsistencia
{
    public class DeleteAsistenciaCommand : IRequest<Response<bool>>
    {
       public Guid AsistenciaId { get; set; }
    }

    public class DeleteAsistenciaCommandHadler : IRequestHandler<DeleteAsistenciaCommand, Response<bool>>
    {
        private readonly IRepositoryAsync<Asistencia> _repositoryAsistencia;

        public DeleteAsistenciaCommandHadler(IRepositoryAsync<Asistencia> repositoryAsistencia)
        {
            _repositoryAsistencia = repositoryAsistencia;
        }
        public async Task<Response<bool>> Handle(DeleteAsistenciaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var asistencia = await _repositoryAsistencia.GetByIdAsync(request.AsistenciaId, cancellationToken);
                if (asistencia == null)
                    throw new ApiException("Error al rechazar retiro de la charla");

                await _repositoryAsistencia.DeleteAsync(asistencia, cancellationToken);

                return new Response<bool>(true);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }
    }
}
