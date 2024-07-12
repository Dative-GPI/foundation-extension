using System;
using Bones.Repository.Interfaces;

namespace Foundation.Extension.Context.DTOs
{
    public class ApplicationDTO: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public string AdminJWT { get; set; }
        public bool Disabled { get; set; }
    }
}