using System;

namespace Foundation.Extension.Gateway.Models
{
    public class RequestContext
    {
        public Guid ApplicationId { get; set; }
        public Guid? ActorId { get; set; }
        public Guid? SourceId { get; set; }

        public string LanguageCode { get; set; }
        public string Jwt { get; set; }
    }
}