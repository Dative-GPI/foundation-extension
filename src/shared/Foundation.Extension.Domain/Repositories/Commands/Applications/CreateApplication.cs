using System;

namespace Foundation.Extension.Domain.Repositories.Commands
{
    public class CreateApplication
    {
        public Guid ApplicationId { get; set; }   
        public string Host { get; set; }
        public string AdminJWT { get; set; }
    }
}