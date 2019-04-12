using System;
using System.Diagnostics;

using PassXYZ.Utils;

using GUMIWA.Models;

namespace GUMIWA.ViewModels
{

    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public JsonData<JsonAttachment> Attachment { get; set; }

        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Title;
            Item = item;
        }


        /// <summary>
        /// Return the Markdown text
        /// </summary>
        /// <returns></returns>
        public string GetMarkdownText()
        {
            return Item.Notes;
        }
    }
}
