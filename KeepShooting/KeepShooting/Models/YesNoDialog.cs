using CocosSharp;
using System;

namespace KeepShooting.Models
{
    public class YesNoDialog : ModalLayer
    {
        public const int TAG = -10102220;

        CCMenuItem _yes = null;
        CCMenuItem _no = null;
        CCMenu _menu = null;

        CCMenuItemLabel _title = null;
        CCMenuItemLabel _content = null;

        public YesNoDialog(string title, string content, Action<object> yes, Action<object> no)
        {

            _title = new MenuItemNoActionLable(new CCLabel(title, "arial", 40))
            {
                //Position=new CCPoint(GlobalGameData.Window_Center_X,GlobalGameData.Window_Center_Y+50),
                Color = CCColor3B.White,

            };
            _content = new MenuItemNoActionLable(new CCLabel(content, "arial", 20))
            {
                //Position=new CCPoint(GlobalGameData.Window_Center_X,GlobalGameData.Window_Center_Y),
                Color = CCColor3B.White,

            };
            _yes = new CCMenuItemLabel(new CCLabel("はい", "arial", 50)
            {
                // Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y)
            }, yes);
            _no = new CCMenuItemLabel(new CCLabel("いいえ", "arial", 50)
            {
                //Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y)
            }, no);

            int margin = 10;
            var drawNode_height = _title.ContentSize.Height + _content.ContentSize.Height + _yes.ContentSize.Height + 3 * margin;
            var drawNode_width = 2 * margin + _content.ContentSize.Width;

            var _drawNode = new CCDrawNode();
            _drawNode.DrawRect(new CCRect(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y, drawNode_width, drawNode_height), CCColor4B.White);
            //            _backGround = _drawNode;


        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var n = new CCMenuItem[] { _title, _content, _yes, _no };

            foreach (var m in n)
            {
                //m.ZOrder = 500;
            }

            _menu = new ModalMenu()
            {
                Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y - 40),
            };

            foreach (var item in n)
            {
                _menu.AddChild(item);
            }

            //          AddChild(_backGround);
            _menu.AlignItemsVertically(30);
            //            _menu.AlignItemsHorizontally(20);

            layer.AddChild(_menu/*,layer.ZOrder+1*/);
            CCLog.Log($"[OreOreLog]:[layer]{layer.ZOrder}");
            CCLog.Log($"[OreOreLog]:[menu]{_menu.ZOrder}");


        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            //            Close();
        }

        public override void Close()
        {
            base.Close();

        }
    }
}
