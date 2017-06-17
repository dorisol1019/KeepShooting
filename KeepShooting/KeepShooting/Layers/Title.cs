using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocosSharp;
using KeepShooting.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace KeepShooting.Layers
{
    public class Title : BaseLayer
    {

        public Title() //: base(new CCColor4B(0x55, 0x55, 0x55))
        {
            CCAudioEngine.SharedEngine.BackgroundMusicVolume = 0.5f;
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var title = new CCSprite("Images/Title/title_old")
            {
                Position = new CCPoint(ContentSize.Center.X,/*380-150/2*/ContentSize.Center.Y)
            };
            AddChild(title);

 
            CCAudioEngine.SharedEngine.PlayBackgroundMusic("Music/Title/bgm_maoudamashii_8bit24", true);

            var gameMenuItem = new CCMenuItemLabel(new CCLabel("はじめる", "Arial", 50),
                _ =>
               {
                   ChangeScene(new Loading(new GameAssetsLoader(new GameAssets())));
               }
            );
            var rankingMenuItem = new CCMenuItemLabel(new CCLabel("らんきんぐ", "Arial", 50),
                _ =>
                {
                    ChangeScene(new Loading(new RankingAssetsLoader()));
                }
            );
            var helpMenuItem = new CCMenuItemLabel(new CCLabel("へるぷ", "Arial", 50),
                _ =>
                {
                    var osiItem = new CCMenuItemLabel(new CCLabel("おーぷんそーすらいぶらり", "Arial", 40),
                        __=> 
                        {
                            NavigateToWebView("OpenSourceLibrary.html");
                        })
                    ;
                    var sozaiItem = new CCMenuItemLabel(new CCLabel("つかったそざい", "Arial", 40),
                        __=>
                        {
                            NavigateToWebView("UsingMaterial.html");
                        });
                    var closeItem = new CCMenuItemLabel(new CCLabel("とじる", "Arial", 40),
                        __ =>
                        {
                            var dialog = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                            dialog.Close();
                            RemoveChild(dialog);
                        });
                    var menu = new ModalMenu(osiItem, sozaiItem,closeItem)
                    {
                        Position = ContentSize.Center,
                    };
                    menu.AlignItemsVertically(40);
                    var modalDialog = new ModalMenuDialog(menu);

                    AddChild(modalDialog, 1, ModalMenuDialog.TAG);
                });

            var titleMenuItems = new[] { gameMenuItem, rankingMenuItem, helpMenuItem };

            var titleMenu = new CCMenu(titleMenuItems)
            {
                Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Height / 4),
            };
            titleMenu.AlignItemsVertically(30);
            AddChild(titleMenu);

        }

       public static ICommand NavigateToWebViewCommand { get; set; }

        private void NavigateToWebView(string url)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NavigateToWebViewCommand.Execute(url);
            });
        }
    }
}
