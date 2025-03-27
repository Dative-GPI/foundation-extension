using System;
using Bones.Flow;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Gateway
{
    public class FileQuery : IRequest<FileDetails>
    {
        public Guid FileId { get; set; }
    }
}