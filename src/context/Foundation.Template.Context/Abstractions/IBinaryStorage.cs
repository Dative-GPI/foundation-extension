using System.Threading.Tasks;

namespace Foundation.Template.Context.Abstractions
{
    public interface IBinaryStorage
    {
        Task Store(string path, byte[] data);
        Task<byte[]> Get(string path);
        Task<string> ComputePath(byte[] data);
    }
}