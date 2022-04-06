using System.Collections.ObjectModel;
using CorpPass.Services;

namespace CorpPass.ViewModels
{
    public class IconItem : BaseViewModel
    {
        public string Name { get; set; }
    }

    internal class PopupIconsViewModel
    {
        public PopupIconsViewModel()
        {
            IconsList = new ObservableCollection<IconItem>();
            InitIconsSet();
        }

        public ObservableCollection<IconItem> IconsList { get; private set; }

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