using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.Features.Files.UploadImages;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Presentation.WebApi.Controllers.v1
{
    public class FilesController : BaseApiController
    {
        [HttpPost("images")]
        public async Task<IActionResult> UploadImages(IList<IFormFile> formFiles)
        {
            var uploadImagesCommand = new UploadImagesCommand();
            foreach(var formFile in formFiles)
            {
                var file = new FileRequest
                {
                    Content = formFile.OpenReadStream(),
                    Name = formFile.FileName,
                    ContentType = formFile.ContentType
                };
                uploadImagesCommand.Files.Add(file);
            }
            return Ok(await Mediator.Send(uploadImagesCommand));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage(DeleteImagesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
