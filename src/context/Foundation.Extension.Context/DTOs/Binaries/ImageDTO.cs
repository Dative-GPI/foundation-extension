using System;

using Bones.Repository.Interfaces;
using Foundation.Extension.Domain.Enums;

namespace Foundation.Extension.Context.DTOs
{
    public class ImageDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public string ThumbnailPath { get; set; }
        public string BlurHash { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool Disabled { get; set; }
        public Scope Scope { get; set; }
        public Guid? ApplicationId { get; set; }
        public ApplicationDTO Application { get; set; }
        public Guid? OrganisationId { get; set; }
        public Guid? UserId { get; set; }
    }
}