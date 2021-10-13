using MediatR;
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
    public class UpdateCharlaCommand : IRequest<Response<Guid>>
    {
        public Guid CharlaId { get; set; }
        public string NombreCharla { get; set; }
        public string DescripcionCharla { get; set; }
        public FileRequest ImgFile { get; set; }
    }

    public class UpdateCharlaCommandHandler : IRequestHandler<UpdateCharlaCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Charla> _repositoryCharla;
        private readonly IFileStorageService _fileStorageService;

        public UpdateCharlaCommandHandler(IRepositoryAsync<Charla> repositoryCharla, IFileStorageService fileStorageService)
        {
            _repositoryCharla = repositoryCharla;
            _fileStorageService = fileStorageService;
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
                if (request.ImgFile != null)
                {
                    var result = await _fileStorageService.DeleteAsync(charla.UrlImage, charla.CharlaId);
                    if (result)
                    {
                        var urlImg = await _fileStorageService.UploadSingleAsync(request.ImgFile, charla.CharlaId);
                        charla.UrlImage = urlImg;
                    }
                }
                
                await _repositoryCharla.UpdateAsync(charla, cancellationToken);
                return new Response<Guid>(charla.CharlaId, "Se actualizó la charla correctamente");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
