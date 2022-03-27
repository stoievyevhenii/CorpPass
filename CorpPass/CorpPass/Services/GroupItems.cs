using CorpPass.Models;
using System.Collections.Generic;
using System.Linq;

namespace CorpPass.Services
{
    internal class GroupItems
    {
        private List<CollectionListItem> groupItems;

        public GroupItems()
        {
            InitGroupIcons();
        }

        private void InitGroupIcons()
        {
            groupItems = new List<CollectionListItem>()
            {
                new GroupItem(){ Name ="Work", Icon = "icon_work" },
                new GroupItem(){ Name ="Education", Icon = "icon_education" },
                new GroupItem(){ Name ="Home", Icon = "icon_home" }
            };
        }

        public List<CollectionListItem> GetGroupsItems()
        {
            return groupItems;
        }

        public List<string> GetGroupsNameList()
        {
            var groupsName = new List<string>();

            foreach (var group in groupItems)
            {
                groupsName.Add(group.Name);
            }

            return groupsName;
        }

        public GroupItem GetItemByName(string name)
        {
            var groupsList = GetGroupsItems();
            var item = groupsList.Where(i => i.Name == name).FirstOrDefault();

            return new GroupItem()
            {
                Name = item.Name,
                Icon = item.Icon
            };
        }
    }
}