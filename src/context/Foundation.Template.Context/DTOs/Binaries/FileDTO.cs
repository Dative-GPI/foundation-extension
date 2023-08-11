using System;

using Bones.Repository.Interfaces;

using Foundation.Template.Domain.Enums;

namespace Foundation.Template.Context.DTOs
{
    public class FileDTO : IEntity<Guid>, IDTO
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public Scope Scope { get; set; }
        public Guid? ApplicationId { get; set; }
        public ApplicationDTO Application { get; set; }
        public Guid? OrganisationId { get; set; }
        public Guid? UserId { get; set; }
        public bool Disabled { get; set; }
    }
}