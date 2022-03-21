using System.Collections.Generic;

namespace CorpPass.Services
{
    internal class IconsSet
    {
        private List<string> icons;

        public IconsSet()
        {
            InitIconsSet();
        }

        private void InitIconsSet()
        {
            icons = new List<string>()
            {
                "icon_apple_logo",
                "icon_autodesk",
                "icon_behance",
                "icon_dribbble",
                "icon_facebook",
                "icon_facebook_messenger",
                "icon_flickr",
                "icon_github",
                "icon_google",
                "icon_instagram",
                "icon_linkedin",
                "icon_pinterest",
                "icon_reddit",
                "icon_spotify",
                "icon_stack_overflow",
                "icon_telegram_app",
                "icon_tiktok",
                "icon_twitter",
                "icon_whatsapp",
                "icon_wordpress",
                "icon_youtube_logo"
            };
        }

        public List<string> GetIconsSet()
        {
            return icons;
        }
    }
}
