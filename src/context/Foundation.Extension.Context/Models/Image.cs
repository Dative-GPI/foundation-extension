namespace Foundation.Extension.Context.Models
{
    public class Image
    {
        public byte[] Data { get; set; }
        public string BlurHash { get; set; }
        public byte[] Thumbnail { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}