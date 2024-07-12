using System;

namespace Foundation.Extension.Domain.Models
{
    public class ApplicationDetails
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public string AdminJWT { get; set; }
        public bool Disabled { get; set; }
    }
}