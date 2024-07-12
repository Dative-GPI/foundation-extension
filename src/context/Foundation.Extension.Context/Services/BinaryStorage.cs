using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Azure.Storage.Blobs;

using Foundation.Extension.Context.Abstractions;
using Foundation.Extension.Context.Configurations;

namespace Foundation.Extension.Context.Services
{
    public class BinaryStorage : IBinaryStorage
    {
        private string _connectionString;
        private MD5 _md5;
        private FileConfiguration _config;

        public BinaryStorage(IConfiguration configuration,
            IOptions<FileConfiguration> options)
        {
            _md5 = MD5.Create();
            _config = options.Value;
            _connectionString = configuration.GetConnectionString("AZURE_STORAGE");
        }

        public async Task<byte[]> Get(string path)
        {
            var client = await Init();

            var blob = client.GetBlobClient(path);

            var blobResult = await blob.DownloadContentAsync();

            return blobResult.Value.Content.ToArray();
        }

        public async Task Store(string path, byte[] data)
        {
            var client = await Init();

            BlobClient blob = client.GetBlobClient(path);

            var exist = await blob.ExistsAsync();

            if (!exist)
            {
                using var stream = new MemoryStream(data);
                await blob.UploadAsync(stream);
            }
        }

        private async Task<BlobContainerClient> Init()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_config.ContainerName);

            await containerClient.CreateIfNotExistsAsync();

            return containerClient;
        }

        public async Task<string> ComputePath(byte[] data)
        {
            using var memoryStream = new MemoryStream(data);

            var hash = await _md5.ComputeHashAsync(memoryStream);

            var path = string.Join("/", hash
                // on prend le hash qu'on affiche en valeur hexa (2 char)
                .Select((x, i) => new { Value = x.ToString("X2"), Index = i })
                // on group les morceaux de string par 4 (8 char)
                .GroupBy(i => i.Index / 4)
                // on les merge et on les join avec un "/"
                .Select(g => String.Concat(
                    g.Select(i => i.Value))
                ));

            return path;
        }
    }
}