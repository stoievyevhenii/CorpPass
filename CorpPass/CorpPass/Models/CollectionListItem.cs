using Xamarin.Forms;

namespace CorpPass.Models
{
    public class CollectionListItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public Command ItemCommand { get; set; }
    }
}
