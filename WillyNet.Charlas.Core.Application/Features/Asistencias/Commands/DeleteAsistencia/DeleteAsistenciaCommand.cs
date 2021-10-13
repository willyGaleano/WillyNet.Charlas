using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces;
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
        private readonly IRepositoryAsync<Control> _repositoryControl;
        private readonly ITransactionDb _transactionDb;

        public DeleteAsistenciaCommandHadler(IRepositoryAsync<Asistencia> repositoryAsistencia,
            IRepositoryAsync<Control> repositoryControl, ITransactionDb transactionDb
            )
        {
            _repositoryAsistencia = repositoryAsistencia;
            _repositoryControl = repositoryControl;
            _transactionDb = transactionDb;
        }
        public async Task<Response<bool>> Handle(DeleteAsistenciaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var asistencia = await _repositoryAsistencia.GetByIdAsync(request.AsistenciaId, cancellationToken);
                if (asistencia == null)
                    throw new ApiException("Error al rechazar retiro del evento.");

                await _repositoryAsistencia.DeleteAsync(asistencia, cancellationToken);                

                _transactionDb.DbContextTransaction.Commit();
                return new Response<bool>(true, "Se canceló el evento.");
            }
            catch(Exception ex)
            {
                _transactionDb.DbContextTransaction.Rollback();
                throw new Exception(ex.Message, ex);
            }
            
        }
    }
}
