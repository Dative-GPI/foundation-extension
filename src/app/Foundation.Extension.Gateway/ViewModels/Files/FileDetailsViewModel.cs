using System;

namespace Foundation.Extension.Gateway.ViewModels
{
    public class FileDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Label { get; set; }

        public long Size { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}