using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WillyNet.Charlas.Core.Application.DTOs.AzureBlobStorage;
using WillyNet.Charlas.Core.Application.Interfaces;
using WillyNet.Charlas.Core.Application.Parameters;

namespace WillyNet.Charlas.Infraestructure.Shared.Services.AzureServices
{
    public class BlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<UrlsDto> UploadAsync(ICollection<FileRequest> files, Guid id)
        {
            if (files == null || files.Count == 0)
            {
                return null;
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient("imagescharlascontainer");


            var urls = new List<string>();


            foreach (var file in files)
            {
                var blobClient = containerClient.GetBlobClient(file.GetPathWithFileName(id, "charla"));

                await blobClient.UploadAsync(file.Content, new BlobHttpHeaders { ContentType = file.ContentType });

                urls.Add(blobClient.Uri.ToString());
            }

            return new UrlsDto(urls);
        }

        public async Task<string> UploadSingleAsync(FileRequest file, Guid id, string carpeta)
        {
            if (file == null)
            {
                return null;
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient("imagescharlascontainer");
            var blobClient = containerClient.GetBlobClient(file.GetPathWithFileName(id, carpeta));
            await blobClient.UploadAsync(file.Content, new BlobHttpHeaders { ContentType = file.ContentType });                      

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteAsync(string nameFile)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("imagescharlascontainer");
            var indexIni = nameFile.IndexOf("imagescharlascontainer") + "imagescharlascontainer".Length;
            var nameBlob = nameFile.Substring(indexIni).Trim();
            var blobClient = containerClient.GetBlobClient(nameBlob);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<bool> DeleteBlobAsync(string nameBlob)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("imagescharlascontainer");
            var blobClient = containerClient.GetBlobClient(nameBlob);
            return await blobClient.DeleteIfExistsAsync();
        }
    }
}
