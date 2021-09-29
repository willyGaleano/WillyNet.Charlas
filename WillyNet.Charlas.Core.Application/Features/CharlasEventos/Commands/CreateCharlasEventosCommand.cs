using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Specifications.Estados;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.CharlasEventos.Commands
{
    public class CreateCharlasEventosCommand : IRequest<Response<Guid>>
    {        
        public short Aforo { get; set; }
        public DateTime FechaIni { get; set; }
        public short Duracion { get; set; }                        
        public Guid? CharlaId { get; set; }
        public string NombreCharla { get; set; }
        public string DescripcionCharla { get; set; }
        public string UrlImageCharla { get; set; }
    }

    public class CreateCharlasEventosCommandHandler : IRequestHandler<CreateCharlasEventosCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<CharlaEvento> _repositoryCE;
        private readonly IRepositoryAsync<Charla> _repositoryCharla;
        private readonly IRepositoryAsync<Evento> _repositoryEvento;
        private readonly IRepositoryAsync<Estado> _repositoryEstado;
        private readonly ITransactionDb _transactionDb;

        public CreateCharlasEventosCommandHandler(IRepositoryAsync<CharlaEvento> repositoryCE,
                IRepositoryAsync<Charla> repositoryCharla, IRepositoryAsync<Evento> repositoryEvento,
                IRepositoryAsync<Estado> repositoryEstado, ITransactionDb transactionDb
            )
        {
            _repositoryCE = repositoryCE;
            _repositoryCharla = repositoryCharla;
            _repositoryEvento = repositoryEvento;
            _repositoryEstado = repositoryEstado;
            _transactionDb = transactionDb;
        }

        public async Task<Response<Guid>> Handle(CreateCharlasEventosCommand request, CancellationToken cancellationToken)
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
                        UrlImage = request.UrlImageCharla
                    };

                    await _repositoryCharla.AddAsync(newChrala, cancellationToken);
                }
                else
                {
                    charlaId = (Guid)request.CharlaId;
                }

                var estado = await _repositoryEstado.GetBySpecAsync(new GetByNameSpecification("Disponible"), cancellationToken);
                var newEvento = new Evento
                {
                    EventoId = Guid.NewGuid(),
                    FechaIni = request.FechaIni,
                    Duracion = request.Duracion,
                    FechaFin = request.FechaIni.AddHours(request.Duracion),
                    Aforo = request.Aforo,
                    EstadoId = estado.EstadoId
                };
                await _repositoryEvento.AddAsync(newEvento, cancellationToken);

                var newCharlaEvento = new CharlaEvento
                {
                    CharlaEventoId = Guid.NewGuid(),
                    CharlaId = charlaId,
                    EventoId = newEvento.EventoId
                };

                await _repositoryCE.AddAsync(newCharlaEvento, cancellationToken);

                _transactionDb.DbContextTransaction.Commit();

                return new Response<Guid>(newCharlaEvento.CharlaEventoId, "Se creó correctamente.");
            }
            catch(Exception ex)
            {
                _transactionDb.DbContextTransaction.Rollback();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
