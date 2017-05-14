using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using WhereTo.Models;
using WhereTo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhereTo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllItemsPage : ContentPage
    {
        AllItemsViewModel viewModel;

        public AllItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AllItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Event;
            if (item == null)
                return;

            await Navigation.PushPopupAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Events.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}