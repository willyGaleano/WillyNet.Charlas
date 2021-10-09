using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.AzureBlobStorage;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Parameters;
using WillyNet.Charlas.Core.Application.Wrappers;

namespace WillyNet.Charlas.Core.Application.Features.Files.UploadImages
{
    public class UploadImagesCommand : IRequest<Response<UrlsDto>>
    {
        public ICollection<FileRequest> Files { get; set; } = new List<FileRequest>();
    }

    public class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand, Response<UrlsDto>>
    {
        private readonly IFileStorageService _fileStorageService;

        public UploadImagesCommandHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<Response<UrlsDto>> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
        {
            var urls = await _fileStorageService.UploadAsync(request.Files, Guid.NewGuid());
            return new Response<UrlsDto>(urls, "Imagen guardada correctamente");
        }
    }
}
