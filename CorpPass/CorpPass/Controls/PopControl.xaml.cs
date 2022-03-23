using CorpPass.Services;
using CorpPass.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorpPass.Controls
{
    public partial class PopControl : Popup
    {
        public PopControl()
        {
            InitializeComponent();
            BindingContext = new PopupIconsViewModel();
        }

        void Button_Clicked(object sender, EventArgs args)
        {
            var selectedImageButton = sender as ImageButton;
            Dismiss(selectedImageButton.Source);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Dismiss("icon_lock_color");
        }
    }
}