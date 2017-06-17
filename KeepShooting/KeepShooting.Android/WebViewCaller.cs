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

using KeepShooting;
using Xamarin.Forms;

using KeepShooting.Droid;
using KeepShooting.Models;
using Android.Webkit;

[assembly: Dependency(typeof(WebViewCaller))]
namespace KeepShooting.Droid
{
    public class WebViewCaller : IWebViewCaller
    {
        public void Call()
        {
            if (Forms.Context is MainActivity activity)
            {
                string uri = "file:///android_asset/Content/" + "OpenSourceLibrary.html";
                //Intent intent = new Intent(activity, typeof(WebViewActivity));
                //intent.PutExtra("uri", uri);
                //intent.AddFlags(ActivityFlags.NewTask);
                //activity.StartActivity(intent);

                Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(uri));
                activity.StartActivity(intent);
                //                var dialog = new Dialog(activity);
                //var alert = new AlertDialog.Builder(activity);
                //               dialog.SetContentView(Resource.Layout.MyWebView);
                //              dialog.SetCancelable(false);

                //                var webView = dialog.FindViewById<Android.Webkit.WebView>(Resource.Id.mywebview);
                
                //var webView = new Android.Webkit.WebView(Forms.Context);
                //webView.SetWebViewClient(new WebViewClient());

                //WebSettings webSettings = webView.Settings;
                //webSettings.DefaultTextEncodingName = ("utf-8");
                //webView.LoadUrl(uri);
                //alert.SetView(webView);

                //alert.SetTitle("てすてす");
                //alert.SetPositiveButton("OK",(_,__)=>
                //{
                    
                //});

                //alert.Show();
            }
        }
    }
}