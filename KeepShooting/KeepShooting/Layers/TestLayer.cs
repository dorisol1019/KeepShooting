using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KeepShooting.Models;
using CocosSharp;

namespace KeepShooting.Layers
{
    public class TestLayer : BaseLayer
    {

        CCNode canGoTitleLabel = null;
        CCNode canGoGameLabel = null;

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var ccELG = CCEventListenerGesture.Create();
            ccELG.SetSwipeThreshouldDistance(50f);
            ccELG.OnSwipeing = (swipe) =>
              {
                  canGoTitleLabel.Visible = false;
                  canGoGameLabel.Visible = false;

                  switch (swipe)
                  {
                      case CCEventListenerGesture.SwipeDirection.NONE:
                          break;
                      case CCEventListenerGesture.SwipeDirection.UP:
                          break;
                      case CCEventListenerGesture.SwipeDirection.DOWN:
                          break;
                      case CCEventListenerGesture.SwipeDirection.LEFT:
                          canGoGameLabel.Visible = true;
                          break;
                      case CCEventListenerGesture.SwipeDirection.RIGHT:
                          canGoTitleLabel.Visible = true;

                          break;
                      default:
                          break;
                  }
              };
            //ccELG.OnSwipeEnded = (swipe) =>
            //  {
            //      switch (swipe)
            //      {
            //          case CCEventListenerGesture.SwipeDirection.NONE:
            //              break;
            //          case CCEventListenerGesture.SwipeDirection.UP:
            //              CCLayer layer = new TestLayer2();
            //              AddChild(layer);
            //              break;
            //          case CCEventListenerGesture.SwipeDirection.DOWN:
            //              break;
            //          case CCEventListenerGesture.SwipeDirection.LEFT:
            //              var dialog = new YesNoDialog("移動", "ゲーム画面に移動しますか？",
            //                  (_) =>
            //                  {
            //                      CCLog.Log("[OreOreLog]Push Yes");
            //                      ChangeScene(new Loading(new GameAssetsLoader(new GameAssets())));

            //                      var dialog_ = (YesNoDialog)GetChildByTag(-2);
            //                      dialog_.Close();
            //                  },
            //                  _ =>
            //                  {
            //                      CCLog.Log("[OreOreLog]Push No to _game");

            //                      var dialog_ = (YesNoDialog)GetChildByTag(-2);
            //                      dialog_.Close();
            //                      RemoveChild(dialog_);

            //                  });
            //              AddChild(dialog, 1, -2);
            //              CCLog.Log($"[OreOreLog]:{nameof(TestLayer)}{this.ZOrder}");

            //              break;
            //          case CCEventListenerGesture.SwipeDirection.RIGHT:
            //              var _dialog = new YesNoDialog("移動", "メニュー画面に移動しますか？",
            //                  (_) =>
            //                  {
            //                      CCLog.Log("[OreOreLog]Push Yes");

            //                      ChangeScene(new Loading(new TitleAssetsLoader()));

            //                      var dialog_ = (YesNoDialog)GetChildByTag(-2);
            //                      dialog_.Close();

            //                  },
            //                  (_) =>
            //                  {
            //                      CCLog.Log("[OreOreLog]Push No");

            //                      var dialog_ = (YesNoDialog)GetChildByTag(-2);
            //                      dialog_.Close();
            //                      RemoveChild(dialog_);
            //                  }
            //                  );
            //              AddChild(_dialog, 1, -2);
            //              CCLog.Log($"[OreOreLog]:{nameof(TestLayer)}{this.ZOrder}");
            //              break;
            //          default:
            //              break;
            //      }
            //  };
            AddEventListener(ccELG,this);

            CCLabel label = new CCLabel("てすとだよっ", "arial", 50)
            {
                Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y)
            };

            canGoTitleLabel = new CCLabel("たいとるにいくよ", "arial", 50)
            {
                Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y - 50)
                    ,
                Visible = false
            };
            canGoGameLabel = new CCLabel("げーむにいくよ", "arial", 50)
            {
                Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y - 50)
                    ,
                Visible = false
            };

            //AddChild(label);
            //AddChild(canGoTitleLabel);
            //AddChild(canGoGameLabel);
            

        }
    }
}
