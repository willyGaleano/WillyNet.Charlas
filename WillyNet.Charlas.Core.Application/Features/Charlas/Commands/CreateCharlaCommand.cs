using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Exceptions;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Interfaces.Repository;
using WillyNet.Charlas.Core.Application.Parameters;
using WillyNet.Charlas.Core.Application.Wrappers;
using WillyNet.Charlas.Core.Domain.Entities;

namespace WillyNet.Charlas.Core.Application.Features.Charlas.Commands
{
    public class CreateCharlaCommand : IRequest<Response<Guid>>
    {
        public string NombreCharla { get; set; }
        public string DescripcionCharla { get; set; }
        public FileRequest ImgFile { get; set; }
    }

    public class CreateCharlaCommandHandler : IRequestHandler<CreateCharlaCommand, Response<Guid>>
    {
       
        private readonly IRepositoryAsync<Charla> _repositoryCharla;
        private readonly IFileStorageService _fileStorageService;

        public CreateCharlaCommandHandler(IRepositoryAsync<Charla> repositoryCharla, IFileStorageService fileStorageService)
        {
            _repositoryCharla = repositoryCharla;
            _fileStorageService = fileStorageService;
        }

        public async Task<Response<Guid>> Handle(CreateCharlaCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();
            var urlImg = await _fileStorageService.UploadSingleAsync(request.ImgFile, id);            

            var newCharla = new Charla
            {
                CharlaId = id,
                Nombre = request.NombreCharla,
                Descripcion = request.DescripcionCharla,
                UrlImage = urlImg
            };
            var result = await _repositoryCharla.AddAsync(newCharla, cancellationToken);
            if (result == null)
                throw new ApiException("No se pudo crear la charla");
            return new Response<Guid>(newCharla.CharlaId, "Charla creada correctamente");
        }
    }
}
