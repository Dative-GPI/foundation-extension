using Foundation.Shared;

namespace Foundation.Template.Domain.Models
{
    public class ActionInfos
    {
        public ActionType ActionType { get; set; }
        public string Uri { get; set; }
        public string Path { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
    }
}