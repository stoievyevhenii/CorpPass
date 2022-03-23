using SQLite;

namespace CorpPass.Models
{
    public class Item
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Icon { get; set; }
        [Ignore]
        public Icon IconModel { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
        public string Folder { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }

        public Item GetEmptyItem()
        {
            return new Item()
            {
                Name = "",
                Login = "",
                Password = "",
                Group = "",
                Folder = "",
                IsFavorite = false
            };
        }
    }
}