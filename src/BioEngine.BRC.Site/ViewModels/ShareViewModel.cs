using System;

namespace BioEngine.BRC.Site.ViewModels
{
    public class ShareViewModel
    {
        public Uri Url { get; }
        public string Title { get; }

        public ShareViewModel(string title, Uri url)
        {
            Title = title;
            Url = url;
        }
    }
}
