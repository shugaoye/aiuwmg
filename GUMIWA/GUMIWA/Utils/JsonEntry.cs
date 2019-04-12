using System;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace PassXYZ.Utils
{
    public class JsonItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsProtected { get; set; }
        public bool IsHidden { get; set; }
    }

    public class JsonAttachment
    {
        public string Filename { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public bool IsHyperLink { get; set; }
    }

    public class JsonData<T>
    {
        public string Title { get; set; }
        public ObservableCollection<T> Items { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
