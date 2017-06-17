using KeepShooting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeepShooting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditContentPage : ContentPage
    {
        public CreditContentPage(string contentsUri)
        {
            InitializeComponent();
            string uri = DependencyService.Get<IBaseUrl>().Get() + contentsUri;

            var webView = new WebView()
            {
                Source = uri,
                BackgroundColor = Color.White,
            };

            Content = webView;
            
        }

        
    }
}