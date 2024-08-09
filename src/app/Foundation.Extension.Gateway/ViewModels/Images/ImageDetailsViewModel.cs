using System;

namespace Foundation.Extension.Gateway.ViewModels
{
    public class ImageDetailsViewModel
    {
        public Guid Id { get; set; }
        public string BlurHash { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}