using System;

namespace Foundation.Extension.Gateway.ViewModels
{
    public class CreateApplicationViewModel
    {
        public Guid ApplicationId { get; set; }
        public string Host { get; set; }
        public string AdminJWT { get; set; }
    }
}