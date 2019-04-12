using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AIUWMG.Models;
using AIUWMG.ViewModels;

namespace AIUWMG.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailWebPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailWebPage()
		{
			InitializeComponent ();

            var item = new Item
            {
                Title = "Notes",
                Notes = "This is a secure notes."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
            titleField.Text = "";
            pxNotesView.Markdown = "";
            pxNotesView.VerticalOptions = LayoutOptions.FillAndExpand;
        }

        public ItemDetailWebPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            titleField.Text = viewModel.Item.Title;
            pxNotesView.Markdown = viewModel.GetMarkdownText();
            pxNotesView.VerticalOptions = LayoutOptions.FillAndExpand;
            pxNotesView.JsonData = viewModel.Item.ToString();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if(viewModel.ReloadData == true)
            //{
            //    // Fill data
            //    ProtectedString ps = viewModel.ItemDetail.Get(PxDefs.NotesField);
            //    if (ps != null)
            //    {
            //        pxNotesView.Markdown = ps.ReadString();
            //    }

            //    ps = viewModel.ItemDetail.Get(PxDefs.TitleField);
            //    if (ps != null)
            //    {
            //        titleField.Text = ps.ReadString();
            //    }
            //    viewModel.ReloadData = false;
            //}
        }

        async void OnEditClicked(object sender, EventArgs e)
        {
            // await Navigation.PushModalAsync(new NavigationPage(new NotesPage(viewModel)));
        }
    }
}