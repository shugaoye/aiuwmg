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
	public partial class MarkdownPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public MarkdownPage()
		{
			InitializeComponent ();

            var item = new Item
            {
                Text = "Notes",
                Description = "This is a secure notes."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
            //titleField.Text = "";
            //pxNotesView.Markdown = "";
            //pxNotesView.VerticalOptions = LayoutOptions.FillAndExpand;
        }

        public MarkdownPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            Debug.WriteLine("MarkdownPage(assigning a new ItemDetailViewModel ...)");
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