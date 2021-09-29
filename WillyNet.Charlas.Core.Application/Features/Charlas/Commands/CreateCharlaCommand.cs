using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Charlas.Commands
{
    public class CreateCharlaCommand : IRequest<Response<Guid>>
    {
        public string NombreCharla { get; set; }
        public string DescripcionCharla { get; set; }
        public string UrlImageCharla { get; set; }
    }

    public class CreateCharlaCommandHandler : IRequestHandler<CreateCharlaCommand, Response<Guid>>
    {
       
        private readonly IRepositoryAsync<Charla> _repositoryCharla;

        public CreateCharlaCommandHandler(IRepositoryAsync<Charla> repositoryCharla)
        {
            _repositoryCharla = repositoryCharla;
        }

        public async Task<Response<Guid>> Handle(CreateCharlaCommand request, CancellationToken cancellationToken)
        {
            var newCharla = new Charla
            {
                CharlaId = Guid.NewGuid(),
                Nombre = request.NombreCharla,
                Descripcion = request.DescripcionCharla,
                UrlImage = request.UrlImageCharla
            };
            var result = await _repositoryCharla.AddAsync(newCharla, cancellationToken);
            if (result == null)
                throw new ApiException("No se pudo crear la charla");
            return new Response<Guid>(newCharla.CharlaId, "Charla creada correctamente");
        }
    }
}
