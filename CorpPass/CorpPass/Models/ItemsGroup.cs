using CorpPass.Models;
using System.Collections.Generic;

namespace CorpPass.ViewModels
{
    public class ItemsGroup<T> : List<T>
    {
        public string Name { get; private set; }

        public ItemsGroup(string name, List<T> items) : base(items)
        {
            Name = name;
        }
    }
}