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

using KeepShooting.Models;
using Xamarin.Forms;
using KeepShooting.Droid;
using Android.Graphics;
using System.IO;

[assembly:Dependency(typeof(ShareSNS))]
namespace KeepShooting.Droid
{
    public class ShareSNS:IShareSNS
    {
        public void Post(string text,MemoryStream imageStream)
        {
            byte[] imageByte = imageStream.ToArray();
            //Bitmap b = BitmapFactory.DecodeByteArray(imageByte,0,imageByte.Length);
            var tempFilename = "ScreenShot.png";
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

            var dir = System.IO.Path.Combine(sdCardPath, "jp.dorifru0209.KeepShooting");

            if(!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            var filePath = System.IO.Path.Combine(dir, tempFilename);
            try
            {
                using (var os = new FileStream(filePath, FileMode.Create))
                {
                    os.Write(imageByte, 0, imageByte.Length);
                }
            }
            catch(Exception e)
            {
                e.Message.Max();
                throw;
            }
            //b.Dispose();
            var imageUri = Android.Net.Uri.Parse($"file://{dir}/{tempFilename}");

            

            Intent intent = new Intent(Intent.ActionSend);
            
            intent.PutExtra(Intent.ExtraText, text);
            intent.SetType("image/png");

            
            intent.PutExtra(Intent.ExtraStream, imageUri);
            Xamarin.Forms.Forms.Context.StartActivity(Intent.CreateChooser(intent, "共有"));

            
        }
    }
}