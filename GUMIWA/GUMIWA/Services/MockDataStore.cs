using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GUMIWA.Models;

namespace GUMIWA.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Title = "Google", Username = "test user", Password = "12345", Url = "http://www.google.com", Notes="## The first item. \n\n**This** is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Facebook", Notes="GUMIWA\n\nThe **Facebook** account is not available." },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Amazon", Notes="GUMIWA\n\nThis **is** a hidden field." },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Tencent", Notes="GUMIWA\n\nPlease visit [Github](http://github.com)" },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Alibaba", Notes="GUMIWA\n\n![KeePass](https://keepass.info/images/icons/keepass_512x512.png)" },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Baidu", Notes="GUMIWA\n\n![KeePass banner](https://keepass.info/images/icons/keepass_322x132.png)" },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}