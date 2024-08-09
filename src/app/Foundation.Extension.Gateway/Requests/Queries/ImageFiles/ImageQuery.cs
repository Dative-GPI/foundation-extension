using System;
using Bones.Flow;

using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Gateway
{
    public class ImageQuery : IRequest<ImageDetails>
    {        
        public Guid Id { get; set; }
    }
}