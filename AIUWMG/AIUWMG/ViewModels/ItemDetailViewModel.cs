using System;

using Newtonsoft.Json;

using AIUWMG.Models;

namespace AIUWMG.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }

        /// <summary>
        /// Return the object Item as JSON data
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(Item);
        }

        /// <summary>
        /// Return the Markdown text
        /// </summary>
        /// <returns></returns>
        public string GetMarkdownText()
        {
            return Item.Description;
        }
    }
}
