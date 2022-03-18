namespace CorpPass.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
        public string Folder { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
    }
}