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
    }
}