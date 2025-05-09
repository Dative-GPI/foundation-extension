using Foundation.Shared;

namespace Foundation.Extension.Core.ViewModels
{
    public class ActionInfosViewModel
    {
        public ActionType ActionType { get; set; }
        public string Path { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public string Uri { get; set; }
        public string Color { get; set; }
    }
}