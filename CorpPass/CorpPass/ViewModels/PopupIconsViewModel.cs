using CorpPass.Services;
using System.Collections.ObjectModel;

namespace CorpPass.ViewModels
{
    public class IconItem : BaseViewModel
    {
        public string Name { get; set; }
    }

    internal class PopupIconsViewModel
    {
        public ObservableCollection<IconItem> IconsList { get; private set; }

        public PopupIconsViewModel()
        {
            IconsList = new ObservableCollection<IconItem>();
            InitIconsSet();
        }

        private void InitIconsSet()
        {
            var iconsSetModel = new IconsSet();
            var icons = iconsSetModel.GetIconsSet();

            foreach (var icon in icons)
            {
                var iconItem = new IconItem()
                {
                    Name = icon.Name
                };

                IconsList.Add(iconItem);
            }
        }
    }
}
