using CorpPass.Models;
using System.Collections.Generic;

namespace CorpPass.ViewModels
{
    public class ItemsGroup : List<Item>
    {
        public string Name { get; private set; }

        public ItemsGroup(string name, List<Item> items) : base(items)
        {
            Name = name;
        }
    }
}