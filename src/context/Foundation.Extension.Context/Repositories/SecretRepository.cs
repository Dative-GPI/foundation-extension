using System;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Secrets;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace  Foundation.Extension.Context.Repositories
{
    public class SecretRepository : ISecretRepository
    {
        private CertificateClient _certificateClient;
        private SecretClient _secretClient;
        private ILogger<SecretRepository> _logger;

        public SecretRepository(ILogger<SecretRepository> logger, 
            CertificateClient certificateClient,
            SecretClient secretClient)
        {
            _certificateClient = certificateClient;
            _secretClient = secretClient;

            _logger = logger;
        }

        public async Task Create(string path, string value, SecretType type)
        {
            switch (type)
            {
                case SecretType.Certificate:
                    await _certificateClient.ImportCertificateAsync(new ImportCertificateOptions(Slugify(path), Convert.FromBase64String(value)));
                    break;
                case SecretType.String:
                    await _secretClient.SetSecretAsync(new KeyVaultSecret(Slugify(path), value));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<string> Get(string path, SecretType type)
        {
            string result;
            switch (type)
            {
                case SecretType.Certificate:
                    {
                        var response = await _certificateClient.DownloadCertificateAsync(Slugify(path));
                        result = Convert.ToBase64String(response.Value.Export(X509ContentType.Pfx));
                        break;
                    }
                case SecretType.String:
                    {
                        var response = await _secretClient.GetSecretAsync(Slugify(path));
                        result = response.Value.Value;
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }

            return result;
        }


        private string Slugify(string path)
        {
            return path.Replace("/", "--").Replace(".", "--").Replace("_", "--");
        }
    }
}