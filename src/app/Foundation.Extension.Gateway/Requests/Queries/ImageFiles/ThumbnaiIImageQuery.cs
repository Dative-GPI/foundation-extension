using System;

using Bones.Flow;

namespace Foundation.Extension.Gateway
{
    public class ThumbnailImageQuery : IRequest<byte[]>
    {        
        public Guid Id { get; set; }
    }
}