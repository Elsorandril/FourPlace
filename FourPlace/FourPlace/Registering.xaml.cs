using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace FourPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registering : ContentPage
    {

        Entry email;
        Entry firstn;
        Entry lastn;
        Entry pwd;
        public Registering()
        {
            InitializeComponent();
            this.email = this.FindByName<Entry>("Email");
            this.firstn = this.FindByName<Entry>("FirstN");
            this.lastn = this.FindByName<Entry>("LastN");
            this.pwd = this.FindByName<Entry>("Pwd");
        }

        private async void Register(object sender, EventArgs e)
        {

            APIClient apc = new APIClient();
            TD.Api.Dtos.RegisterRequest LR = new TD.Api.Dtos.RegisterRequest();
            LR.Email = this.email.Text;
            LR.FirstName = this.firstn.Text;
            LR.LastName = this.lastn.Text;
            LR.Password = this.pwd.Text;
            HttpResponseMessage fff = await apc.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/register", LR);
            if (fff.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.Write(fff.Content, "Perso");
                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Email already taken", "OK");
                System.Diagnostics.Debug.Write(fff.Content, "Perso");
            }
        }
    }
}