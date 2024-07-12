using System;

using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Domain.Models
{
    public class FileDetails
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }

        public Scope Scope { get; set; }
        public Guid? OrganisationId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ApplicationId { get; set; }

        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}