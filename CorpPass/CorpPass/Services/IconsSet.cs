using System.Collections.Generic;
using CorpPass.Models;
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

        public Icon GetIconModel(string name)
        {
            var iconsSet = GetIconsSet();
            return iconsSet.Find(i => i.Name == name);
        }

        public List<Icon> GetIconsSet()
        {
            return icons;
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

        private void InitIconsSet()
        {
            icons = new List<Icon>()
            {
                new Icon(){ Name = "icon_1cestart", Accent = Color.FromHex("#faea1c")},
                new Icon(){ Name = "icon_airvpn", Accent = Color.FromHex("#2c9aed")},
                new Icon(){ Name = "icon_android_studio", Accent = Color.FromHex("#457ce0")},
                new Icon(){ Name = "icon_apple_logo", Accent = Color.FromHex("#0883d9")},
                new Icon(){ Name = "icon_applications_office", Accent = Color.FromHex("#ce3906")},
                new Icon(){ Name = "icon_applications_system", Accent = Color.FromHex("#21a3d0")},
                new Icon(){ Name = "icon_autodesk", Accent = Color.FromHex("#209bd6")},
                new Icon(){ Name = "icon_avernote", Accent = Color.FromHex("#5dc920")},
                new Icon(){ Name = "icon_aws", Accent = Color.FromHex("#ff9801")},
                new Icon(){ Name = "icon_azure", Accent = Color.FromHex("#2ea1e4")},
                new Icon(){ Name = "icon_behance", Accent = Color.FromHex("#0053f2")},
                new Icon(){ Name = "icon_credentials_preferences", Accent = Color.FromHex("#1985d9")},
                new Icon(){ Name = "icon_discord", Accent = Color.FromHex("#2a7ec3")},
                new Icon(){ Name = "icon_dribbble", Accent = Color.FromHex("#eb528c")},
                new Icon(){ Name = "icon_facebook", Accent = Color.FromHex("#0f89e2")},
                new Icon(){ Name = "icon_feedly", Accent = Color.FromHex("#28ae6f")},
                new Icon(){ Name = "icon_flickr", Accent = Color.FromHex("#063ca8")},
                new Icon(){ Name = "icon_github", Accent = Color.FromHex("#f2f2f2")},
                new Icon(){ Name = "icon_google", Accent = Color.FromHex("#e13e33")},
                new Icon(){ Name = "icon_google_chrome", Accent = Color.FromHex("#42a2ff")},
                new Icon(){ Name = "icon_google_cloud", Accent = Color.FromHex("#fabc08")},
                new Icon(){ Name = "icon_instagram", Accent = Color.FromHex("#c52155")},
                new Icon(){ Name = "icon_jenkins", Accent = Color.FromHex("#d53833")},
                new Icon(){ Name = "icon_jetbrains", Accent = Color.FromHex("#f08a3c")},
                new Icon(){ Name = "icon_linkedin", Accent = Color.FromHex("#0071ae")},
                new Icon(){ Name = "icon_lock_color", Accent = Color.FromHex("#ffffff")},
                new Icon(){ Name = "icon_microsoft_outlook_2019", Accent = Color.FromHex("#0a69c7")},
                new Icon(){ Name = "icon_microsoft_planner_2019", Accent = Color.FromHex("#107c42")},
                new Icon(){ Name = "icon_moodle", Accent = Color.FromHex("#ffab40")},
                new Icon(){ Name = "icon_pinterest", Accent = Color.FromHex("#da0021")},
                new Icon(){ Name = "icon_reddit", Accent = Color.FromHex("#f24100")},
                new Icon(){ Name = "icon_slack", Accent = Color.FromHex("#ecb22e")},
                new Icon(){ Name = "icon_spotify", Accent = Color.FromHex("#1ccc5b")},
                new Icon(){ Name = "icon_stack_overflow", Accent = Color.FromHex("#e87921")},
                new Icon(){ Name = "icon_teamviewer", Accent = Color.FromHex("#0973d5")},
                new Icon(){ Name = "icon_telegram_app", Accent = Color.FromHex("#28a7e1")},
                new Icon(){ Name = "icon_thunderbird", Accent = Color.FromHex("#304aa4")},
                new Icon(){ Name = "icon_tiktok", Accent = Color.FromHex("#e21c4e")},
                new Icon(){ Name = "icon_twitter", Accent = Color.FromHex("#1c99e6")},
                new Icon(){ Name = "icon_viber", Accent = Color.FromHex("#9179ee")},
                new Icon(){ Name = "icon_virtualbox", Accent = Color.FromHex("#24375b")},
                new Icon(){ Name = "icon_visual_studio", Accent = Color.FromHex("#944ed2")},
                new Icon(){ Name = "icon_visual_studio_code_2019", Accent = Color.FromHex("#0288d1")},
                new Icon(){ Name = "icon_vmware_workstation", Accent = Color.FromHex("#fd9206")},
                new Icon(){ Name = "icon_whatsapp", Accent = Color.FromHex("#279c1c")},
                new Icon(){ Name = "icon_windows_10", Accent = Color.FromHex("#31abed")},
                new Icon(){ Name = "icon_wordpress", Accent = Color.FromHex("#006e92")},
                new Icon(){ Name = "icon_youtrack", Accent = Color.FromHex("#726ffa")},
                new Icon(){ Name = "icon_youtube_logo", Accent = Color.FromHex("#f20000")},
                new Icon(){ Name = "icon_youtube_studio", Accent = Color.FromHex("#eb313f")},
            };
        }
    }
}