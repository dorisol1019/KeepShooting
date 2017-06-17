using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocosSharp;
using KeepShooting.Layers;
using KeepShooting.Models;
namespace KeepShooting
{
    static public class GameDelegate
    {
        public static void LoadGame(object sender, EventArgs e)
        {
            CCLog.Logger = (format, args) =>
            {
                System.Diagnostics.Debug.WriteLine("CocosSharpTests: " + format, args);
            };



            var gameView = sender as CCGameView;
            if (gameView == null) return;
#if DEBUG
            gameView.Stats.Enabled = true;
            gameView.Stats.Scale = 3;
#endif

            //            gameView.DesignResolution = new CCSizeI(224, 380);
            gameView.DesignResolution = new CCSizeI(500, 650);
            gameView.ResolutionPolicy = CCViewResolutionPolicy.ShowAll;

            var contentSearchPaths = new List<string> { "Images", "Sounds" ,"Fonts"};
            gameView.ContentManager.SearchPaths = contentSearchPaths;
            gameView.ContentManager.RootDirectory = "Content";
            var gameScene = new CCScene(gameView);
            gameScene.AddLayer(new Loading(new TitleAssetsLoader()));

            gameView.RunWithScene(gameScene);


        }
    }
}
