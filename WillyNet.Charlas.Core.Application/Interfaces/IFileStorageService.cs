using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.AzureBlobStorage;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Core.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<UrlsDto> UploadAsync(ICollection<FileRequest> files, Guid id);
        Task<string> UploadSingleAsync(FileRequest file, Guid id, string carpeta);
        Task<bool> DeleteAsync(string nameFile, Guid id);
        Task<bool> DeleteBlobAsync(string nameBlob);
    }
}
