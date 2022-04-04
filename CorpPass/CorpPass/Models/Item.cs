using SQLite;

namespace CorpPass.Models
{
    public class Item
    {
        public string Created { get; set; }
        public string Description { get; set; }

        public string Folder { get; set; }

        public string Group { get; set; }

        public string Icon { get; set; }

        [Ignore]
        public Icon IconModel { get; set; }

        [PrimaryKey]
        public string Id { get; set; }

        public bool IsFavorite { get; set; }
        public bool IsLeaked { get; set; } //TODO ADD leaked status

        public string LastModified { get; set; } //TODO ADD last modified date
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public double PasswordScore { get; set; }

        public Item GetEmptyItem()
        {
            return new Item()
            {
                Name = "",
                Login = "",
                Password = "",
                Group = "",
                Folder = "",
                IsFavorite = false,
                IsLeaked = false
            };
        }
    }
}