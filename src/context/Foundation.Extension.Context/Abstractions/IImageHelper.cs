using System.Threading.Tasks;
using Foundation.Extension.Context.Models;

namespace Foundation.Extension.Context.Abstractions
{
    public interface IImageHelper
    {
        Task<Image> Compute(byte[] data, int maxSize);
    }
}