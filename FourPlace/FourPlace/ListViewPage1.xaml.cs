using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Collections.Generic;
using TD.Api.Dtos;
using Common.Api.Dtos;

namespace FourPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage1 : ContentPage
    {
        public ObservableCollection<TD.Api.Dtos.PlaceItemSummary> Items { get; set; }

        public ListViewPage1()
        {
            InitializeComponent();

            /*Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };*/
            /*TD.Api.Dtos.PlaceItemSummary PLS2 = new TD.Api.Dtos.PlaceItemSummary();
            PLS2.Title = "dddd";*/
            MyListView.ItemsSource = this.Items;
            getLocations();
        }

        

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;


            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            System.Diagnostics.Debug.Write(((PlaceItemSummary)e.Item).Id, "Perso");
            await this.Navigation.PushModalAsync(new PlaceDetail(((PlaceItemSummary)e.Item).Id));
        }

        public async void getLocations()
        {
            APIClient apc = new APIClient();
            HttpResponseMessage fff = await apc.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/places");
            ObservableCollection<PlaceItemSummary> LPIS=(await apc.ReadFromResponse<Response<ObservableCollection<PlaceItemSummary>>>(fff)).Data;
                System.Diagnostics.Debug.Write(fff.Content, "Perso");
            MyListView.ItemsSource = LPIS;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //TODO jkhjk    
            await this.Navigation.PushModalAsync(new AddPlace());
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            //TODO jkhjk    
            await this.Navigation.PushModalAsync(new AddPlace());
        }

    }
}
