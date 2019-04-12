using System;
using System.Collections.ObjectModel;
using System.Reflection;

using PassXYZ.Utils;

namespace AIUWMG.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// Return the object Item as JSON data
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            JsonData<JsonItem> entry = new JsonData<JsonItem>();
            entry.Items = new ObservableCollection<JsonItem>();
            entry.Title = this.Title;
            foreach (PropertyInfo prop in typeof(Item).GetProperties())
            {
                var jsonItem = new JsonItem { Key = prop.Name, IsProtected = false, IsHidden = false };
                var objectValue = prop.GetValue(this, null);
                if (objectValue != null) { jsonItem.Value = objectValue.ToString(); }
                entry.Items.Add(jsonItem);
            }

            return entry.ToString();
        }
    }
}