﻿using CorpPass.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CorpPass.ViewModels
{
    public class IconItem : BaseViewModel
    {
        public string Name { get; set; }
    }

    internal class PopupIconsViewModel
    {
        public ObservableCollection<IconItem> IconsList{ get; private set; }

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
                    Name = icon
                };

                IconsList.Add(iconItem);
            }
        }
    }
}
