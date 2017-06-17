using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using CocosSharp;
using KeepShooting;

namespace KeepShooting.Droid
{
    [Activity(Label = "@string/app_name",
        Theme = "@style/MyTheme",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleInstance,
        AlwaysRetainTaskState = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)
        ]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity//Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);



            //RequestWindowFeature(WindowFeatures.NoTitle);
            //var linearLayout = new LinearLayout(this)
            //{
            //    Orientation = Orientation.Vertical,

            //};

            //// ゲーム表示用コントロール作成
            //var gameView = new CCGameView(this);

            //// ゲーム起動
            //if (gameView != null)
            //    gameView.ViewCreated += GameDelegate.LoadGame;

            //gameView.SaveEnabled = true;
            //linearLayout.AddView(gameView);
            //SetContentView(linearLayout);

            ////global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            var decorView = Window.DecorView;
            SystemUiFlags Flag = SystemUiFlags.HideNavigation;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
            {
                Flag |= SystemUiFlags.Fullscreen;
            }
            if(Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                Flag |= SystemUiFlags.ImmersiveSticky;
            }

            decorView.SystemUiVisibility = (StatusBarVisibility)Flag;
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
//            var Visivillity = Window.DecorView.SystemUiVisibility;
        }
    }
}