using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace FourPlace
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Entry id;
        Entry pwd;
        public MainPage()
        {
            InitializeComponent(); 
            Check_Permissions();
            this.id = this.FindByName<Entry>("ID");
            this.pwd = this.FindByName<Entry>("Pwd");
        }

        public async void  Check_Permissions()
        {
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            APIClient apc = new APIClient();
            TD.Api.Dtos.LoginRequest LR = new TD.Api.Dtos.LoginRequest();
            LR.Email = this.id.Text;
            LR.Password = this.pwd.Text;
            HttpResponseMessage fff = await apc.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/login", LR);
            if (fff.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.Write(fff.Content, "Perso");
                await this.Navigation.PushModalAsync(new ListViewPage1());
            }
            else
            {
                await DisplayAlert("Error", "You got the wrong login but we still let you enter", "OK");
                System.Diagnostics.Debug.Write(fff.Content, "Perso");
                await this.Navigation.PushModalAsync(new ListViewPage1());
            }
        }

        private async void Register(object sender, EventArgs e)
        {
                await this.Navigation.PushModalAsync(new Registering());
        }
    }
}
