using CorpPass.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CorpPass.Services
{
    internal class IconsSet
    {
        private List<Icon> icons;

        public IconsSet()
        {
            InitIconsSet();
        }

        private void InitIconsSet()
        {
            icons = new List<Icon>()
            {
                new Icon(){ Name = "icon_apple_logo", Accent = Color.FromHex("#0883d9")},
                new Icon(){ Name = "icon_autodesk", Accent = Color.FromHex("#209bd6")},
                new Icon(){ Name = "icon_behance", Accent = Color.FromHex("#0053f2")},
                new Icon(){ Name = "icon_dribbble", Accent = Color.FromHex("#eb528c")},
                new Icon(){ Name = "icon_facebook", Accent = Color.FromHex("#0f89e2")},
                
                new Icon(){ Name = "icon_flickr", Accent = Color.FromHex("#063ca8")},
                new Icon(){ Name = "icon_github", Accent = Color.FromHex("#f2f2f2")},
                new Icon(){ Name = "icon_google", Accent = Color.FromHex("#e13e33")},
                new Icon(){ Name = "icon_instagram", Accent = Color.FromHex("#c52155")},
                new Icon(){ Name = "icon_linkedin", Accent = Color.FromHex("#0071ae")},
                new Icon(){ Name = "icon_pinterest", Accent = Color.FromHex("#da0021")},
                new Icon(){ Name = "icon_reddit", Accent = Color.FromHex("#f24100")},

                new Icon(){ Name = "icon_spotify", Accent = Color.FromHex("#1ccc5b")},
                new Icon(){ Name = "icon_stack_overflow", Accent = Color.FromHex("#e87921")},
                new Icon(){ Name = "icon_telegram_app", Accent = Color.FromHex("#28a7e1")},
                new Icon(){ Name = "icon_tiktok", Accent = Color.FromHex("#e21c4e")},
                new Icon(){ Name = "icon_twitter", Accent = Color.FromHex("#1c99e6")},
                new Icon(){ Name = "icon_whatsapp", Accent = Color.FromHex("#279c1c")},
                new Icon(){ Name = "icon_wordpress", Accent = Color.FromHex("#006e92")},
                new Icon(){ Name = "icon_youtube_logo", Accent = Color.FromHex("#f20000")},
                new Icon(){ Name = "icon_lock_color", Accent = Color.FromHex("#ffffff")}
            };
        }

        public List<Icon> GetIconsSet()
        {
            return icons;
        }

        public Icon GetIconModel(string name)
        {
            var iconsSet = GetIconsSet();
            return iconsSet.Find(i => i.Name == name);
        }

        public List<Item> SetIconForItem(List<Item> items)
        {
            var iconsSet = GetIconsSet();
            
            foreach (var item in items)
            {
                item.IconModel = iconsSet.Find(x => x.Name == item.Icon);
            }

            return items;
        }
    }
}
