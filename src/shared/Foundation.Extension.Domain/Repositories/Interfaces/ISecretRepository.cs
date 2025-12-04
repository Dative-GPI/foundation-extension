using System.Threading.Tasks;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Domain.Repositories.Interfaces
{
    public interface ISecretRepository
    {
        Task Create(string path, string value, SecretType type);
        Task<string> Get(string path, SecretType type);
    }
}