using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Interfaces.Utilities;
using WillyNet.Charlas.Core.Application.Specifications.Asistencias;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Asistencias.Commands.CreateAsistencia
{
    public class CreateAsistenciaCommand : IRequest<Response<Guid>>
    {
        public string UserAppId { get; set; }
        public Guid CharlaEventoId { get; set; }
        public DateTime FecSesion { get; set; }
    }
    public class CreateAsistenciaCommandHandler : IRequestHandler<CreateAsistenciaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Asistencia> _repositoryAsistencia;
        private readonly IControlUtil _controlUtil;
        private readonly ITransactionDb _transactionDb;

        public CreateAsistenciaCommandHandler(IRepositoryAsync<Asistencia> repositoryAsistencia,
            IControlUtil controlUtil, ITransactionDb transactionDb)
        {
            _repositoryAsistencia = repositoryAsistencia;
            _controlUtil = controlUtil;
            _transactionDb = transactionDb;
        }

        public async Task<Response<Guid>> Handle(CreateAsistenciaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var asistencia = await _repositoryAsistencia
                   .GetBySpecAsync(
                   new GetAsistenciaByUserIdCharlaEveIdEspecification(request.UserAppId, request.CharlaEventoId),
                   cancellationToken
                );
                if (asistencia != null)                    
                    return new Response<Guid>("Ya se registró en esta charla");

                var limiteCharlas = await _controlUtil.CreateOrUpdateCantControl(request.UserAppId, request.FecSesion);
                if (!limiteCharlas)
                    return new Response<Guid>("Superó sus límites de charlas por día");

                var id = Guid.NewGuid();
                var newAsistencia = new Asistencia
                {
                    AsistenciaId = id,
                    CharlaEventoId = request.CharlaEventoId,
                    UserAppId = request.UserAppId,
                    Asistio = false
                };

                var result = await _repositoryAsistencia.AddAsync(newAsistencia, cancellationToken);

                if (result == null)
                    throw new ApiException("No se pudo crear la asistencia");

                await _transactionDb.DbContextTransaction.CommitAsync(cancellationToken);
                return new Response<Guid>(id, "¡Asistencia creada correctamente!");
            }
            catch(Exception ex)
            {
                await _transactionDb.DbContextTransaction.RollbackAsync(cancellationToken);
                throw new ApiException(ex.Message);
            }
        }
    }
}
