using System;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class CreateImage
    {
        public byte[] Data { get; set; }
        public Scope Scope { get; set; }
        public string Label { get; set; }
        public Guid? OrganisationId { get; set; }
        public Guid? ApplicationId { get; set; }
        public Guid? UserId { get; set; }
        public int MaxSize { get; set; } = 200;
    }
}