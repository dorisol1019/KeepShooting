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

using Xamarin.Forms;
using KeepShooting.Models;
using KeepShooting.Droid;

[assembly: Dependency(typeof(BaseUrl))]
namespace KeepShooting.Droid
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/Content/";
        }
    }
}