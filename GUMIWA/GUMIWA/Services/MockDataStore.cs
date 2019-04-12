using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIUWMG.Models;

namespace AIUWMG.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Title = "Google", Username = "test user", Password = "12345", Url = "http://www.google.com", Notes="## The fist item. \n\n**This** is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Facebook", Notes="AIUWMG" },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Amazon", Notes="This is a hidden field." },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Tencent", Notes="http://github.com" },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Alibaba", Notes="1234567890" },
                new Item { Id = Guid.NewGuid().ToString(), Title = "Baidu", Notes="4321" },
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