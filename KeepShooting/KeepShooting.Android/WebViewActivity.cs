using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Webkit;

namespace KeepShooting.Droid
{
    [Activity(Label = "WebViewActivity",ParentActivity =typeof(MainActivity))]
    public class WebViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MyWebView);
            // Create your application here
            WebView webView = FindViewById<WebView>(Resource.Id.mywebview);

            webView.SetWebViewClient(new WebViewClient());
            var uri = Intent.GetStringExtra("uri");

            WebSettings webSettings = webView.Settings;
            webSettings.DefaultTextEncodingName=("utf-8");
            webView.LoadUrl(uri);

            this.Title = "";
            var backButton = FindViewById(Android.Resource.Id.CloseButton);
            
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    //Intent intent = new Intent(this, typeof(MainActivity));
                    //intent.AddFlags(ActivityFlags.SingleTop);
                    //intent.AddFlags(ActivityFlags.ClearTop);
                    //StartActivity(intent);
                    NavigateUpTo(ParentActivityIntent);
                    return false;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}