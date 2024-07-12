using System;
using Bones.Flow;

namespace Foundation.Extension.Gateway
{
    public class RawImageQuery : IRequest<byte[]>
    {        
        public Guid Id { get; set; }
    }
}