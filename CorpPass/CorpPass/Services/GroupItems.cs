
using CorpPass.Models;
using System.Collections.Generic;

namespace CorpPass.Services
{
    class GroupItems
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
                new CollectionListItem(){ Name ="Work", Icon = "icon_work" },
                new CollectionListItem(){ Name ="Education", Icon = "icon_education" },
                new CollectionListItem(){ Name ="Home", Icon = "icon_home" }
            };
        }

        public List<CollectionListItem>  GetGroupsItems()
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
    }
}
