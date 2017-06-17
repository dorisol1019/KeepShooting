using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KeepShooting
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var grid = this.FindByName<Grid>("MainGrid");

            var gameView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ViewCreated = GameDelegate.LoadGame
            };
            grid.Children.Add(gameView, 0, 0);

            KeepShooting.Layers.Title.NavigateToWebViewCommand = new Command(async (uri)=>
            {
                await Navigation.PushAsync(new CreditContentPage(uri as string));
            });
        }
    }
}
