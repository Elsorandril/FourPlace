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
using Common.Api.Dtos;
using Xamarin.Forms.Maps;

namespace FourPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceDetail : ContentPage
    {
        public ObservableCollection<CommentItem> Items { get; set; }
        public PlaceItem Item { get; set; }

        public int ID;
        public PlaceDetail(int ID)
        {
            this.ID = ID;
            InitializeComponent();
            Items = new ObservableCollection<CommentItem>();
            getPlaceItem(ID);
            MyListView.ItemsSource = this.Items;
        }
        protected void OnResume()
        {
            getPlaceItem(this.ID);
        }

        public async void getPlaceItem(int Id)
        {
            APIClient apc = new APIClient();
            HttpResponseMessage fff = await apc.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/places/"+Id);
            PlaceItem PI = (await apc.ReadFromResponse<Response<PlaceItem>>(fff)).Data;
            this.Item = PI;
            TitleLabel.Text = PI.Title;
            DescriptionLabel.Text = PI.Description;
            ImageItem.Source = "https://td-api.julienmialon.com/images/" + PI.ImageId;
            LatLabel.Text = "Latitude:"+PI.Latitude.ToString();
            LonLabel.Text = "Longitude:"+PI.Longitude.ToString();
            MapSpan mapSpan = new MapSpan(new Position(PI.Latitude, PI.Longitude), 0.01, 0.01);
            map = new Map(mapSpan);
            this.Items = ConvertToObservable(PI.Comments);
            MyListView.ItemsSource = ConvertToObservable(PI.Comments);
            System.Diagnostics.Debug.Write(fff.Content, "Perso GetPlaceItem");
        }
        private ObservableCollection<CommentItem> ConvertToObservable(List<CommentItem> li)
        {
            ObservableCollection<CommentItem> OCCI = new ObservableCollection<CommentItem>();
            foreach(CommentItem Ci in li)
            {
                OCCI.Add(Ci);
            }
            return OCCI;
        }
    }
}