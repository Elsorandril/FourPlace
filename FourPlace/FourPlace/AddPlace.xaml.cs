using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FourPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlace : ContentPage
    {
        private String Test;
        public AddPlace()
        {
            InitializeComponent();
        }

        public void OnCreate()
        {
            
        }
        private async void Camera_Button_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
        }
    }
}