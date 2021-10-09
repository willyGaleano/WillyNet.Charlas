using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Wrappers;

namespace WillyNet.Charlas.Core.Application.Features.Files.UploadImages
{
    public class DeleteImagesCommand : IRequest<Response<bool>>
    {
        public string BlobName { get; set; }
    }

    public class DeleteImagesCommandHandler : IRequestHandler<DeleteImagesCommand, Response<bool>>
    {
        private readonly IFileStorageService _fileStorageService;

        public DeleteImagesCommandHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<Response<bool>> Handle(DeleteImagesCommand request, CancellationToken cancellationToken)
        {
            var respt = await _fileStorageService.DeleteBlobAsync(request.BlobName);
            return new Response<bool>(respt, "Blob eliminado correctamente");
        }
    }
}
