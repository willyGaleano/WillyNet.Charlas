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
    public class UpdateCharlaCommand : IRequest<Response<Guid>>
    {
        public Guid CharlaId { get; set; }
        public string NombreCharla { get; set; }
        public string DescripcionCharla { get; set; }
        public string UrlImageCharla { get; set; }
    }

    public class UpdateCharlaCommandHandler : IRequestHandler<UpdateCharlaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;

        public UpdateCharlaCommandHandler(IRepositoryAsync<Charla> repositoryCharla)
        {
            _repositoryCharla = repositoryCharla;
        }
        public async Task<Response<Guid>> Handle(UpdateCharlaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var charla = await _repositoryCharla.GetByIdAsync(request.CharlaId, cancellationToken);
                if (charla == null)
                    throw new ApiException("No existe esa charla");
                charla.Nombre = request.NombreCharla;
                charla.Descripcion = request.DescripcionCharla;
                charla.UrlImage = request.UrlImageCharla;

                await _repositoryCharla.UpdateAsync(charla);
                return new Response<Guid>(charla.CharlaId, "Se actualizó la charla correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
