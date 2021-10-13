using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.EstadosEventos;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Eventos.Commands
{
    public class CreateEventosCommand : IRequest<Response<Guid>>
    {        
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }                        
        public Guid? CharlaId { get; set; }
        public string NombreCharla { get; set; }
        public string DescripcionCharla { get; set; }
        public string UrlImageCharla { get; set; }
    }

    public class CreateEventosCommandHandler : IRequestHandler<CreateEventosCommand, Response<Guid>>
    {        
        private readonly IRepositoryAsync<Charla> _repositoryCharla;
        private readonly IRepositoryAsync<Evento> _repositoryEvento;
        private readonly IRepositoryAsync<EstadoEvento> _repositoryEstadoEvento;
        private readonly ITransactionDb _transactionDb;

        public CreateEventosCommandHandler(
                IRepositoryAsync<Charla> repositoryCharla, IRepositoryAsync<Evento> repositoryEvento,
                IRepositoryAsync<EstadoEvento> repositoryEstadoEvento, ITransactionDb transactionDb
            )
        {            
            _repositoryCharla = repositoryCharla;
            _repositoryEvento = repositoryEvento;
            _repositoryEstadoEvento = repositoryEstadoEvento;
            _transactionDb = transactionDb;
        }

        public async Task<Response<Guid>> Handle(CreateEventosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Guid charlaId;

                if (request.CharlaId == null)
                {
                    charlaId = Guid.NewGuid();
                    var newChrala = new Charla
                    {
                        CharlaId = charlaId,
                        Nombre = request.NombreCharla,
                        Descripcion = request.DescripcionCharla,
                        UrlImage = request.UrlImageCharla,
                        DeleteLog = false
                    };

                    await _repositoryCharla.AddAsync(newChrala, cancellationToken);
                }
                else
                   charlaId = (Guid)request.CharlaId;
                

                var estado = await _repositoryEstadoEvento
                        .GetBySpecAsync(new GetByNameSpecification("Disponible"), cancellationToken);
                var newEvento = new Evento
                {
                    EventoId = Guid.NewGuid(),
                    FechaIni = request.FechaIni,
                    Duracion = request.Duracion,
                    FechaFin = request.FechaIni.AddHours(request.Duracion),
                    Aforo = request.Aforo,
                    EstadoEventoId = estado.EstadoEventoId,
                    CharlaId = charlaId
                };
                await _repositoryEvento.AddAsync(newEvento, cancellationToken);

                _transactionDb.DbContextTransaction.Commit();

                return new Response<Guid>(newEvento.EventoId, "Se creó correctamente el evento.");
            }
            catch(Exception ex)
            {
                _transactionDb.DbContextTransaction.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
