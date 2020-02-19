using Common.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            APIClient apc = new APIClient();
            if (OPWD.Text != null && NPWD.Text != null)
            {
                UpdatePasswordRequest UPR = new UpdatePasswordRequest();
                UPR.NewPassword = NPWD.Text;
                UPR.OldPassword = OPWD.Text;
                HttpResponseMessage fff = await apc.Execute(HttpMethod.Pa, "https://td-api.julienmialon.com/me/password",UPR);
            }
            HttpResponseMessage fff = await apc.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/places");
            ObservableCollection<PlaceItemSummary> LPIS = (await apc.ReadFromResponse<Response<ObservableCollection<PlaceItemSummary>>>(fff)).Data;
            System.Diagnostics.Debug.Write(fff.Content, "Perso");
            MyListView.ItemsSource = LPIS;
        }
    }
}