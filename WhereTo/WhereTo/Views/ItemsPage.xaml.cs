﻿using System;
using Rg.Plugins.Popup.Extensions;
using WhereTo.Models;
using WhereTo.ViewModels;

using Xamarin.Forms;

namespace WhereTo.Views
{
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;
	    public static bool first;
        public ItemsPage()
		{
			InitializeComponent();
		    first = true;
			BindingContext = viewModel = new ItemsViewModel();

		    viewModel.ItemsListView = ItemsListView;
		    ItemsListView.ItemsSource = viewModel.Events;
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
            if (!first) {
                if (viewModel.Events.Count == 0)
                {
                    viewModel.LoadItemsCommand.Execute(null);
                    ItemsListView.RefreshCommand.Execute(null);
                }
            }
		    first = false;
		}
	}
}
