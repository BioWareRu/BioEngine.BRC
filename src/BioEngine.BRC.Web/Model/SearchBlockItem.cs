using System;

namespace BioEngine.BRC.Web.Model
{
    public struct SearchBlockItem
    {
        public string Title { get; }
        public Uri Url { get; }
        public string Text { get; }
        public DateTimeOffset Date { get; }

        public SearchBlockItem(string title, Uri url, string text, DateTimeOffset date)
        {
            Title = title;
            Text = text;
            Date = date;
            Url = url;
        }
    }
}
