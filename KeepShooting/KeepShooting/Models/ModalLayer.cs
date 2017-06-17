using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocosSharp;

namespace KeepShooting.Models
{
    public abstract class ModalLayer : CCLayer
    {

        protected CCLayerColor layer = null;
        protected CCNode _backGround = null;
        public bool IsClosed { get; protected set; } = false;

        public ModalLayer() : base()
        {
            layer = new CCLayerColor(new CCColor4B(0x11, 0x11, 0x11, 0x80));
        }

        public virtual void Close()
        {
            if (IsClosed) return;
            IsClosed = true;
            eventListener.IsSwallowTouches = false;
            //RemoveAllChildren(true);
            RemoveEventListener(eventListener);
            //RemoveFromParent(true);
        }

        CCEventListenerTouchOneByOne eventListener = null;

        protected override void AddedToScene()
        {
            base.AddedToScene();

            eventListener = new CCEventListenerTouchOneByOne()
            {
                IsSwallowTouches = true,
                OnTouchBegan = (touch, __) =>
                {
                    //if (_backGround?.BoundingBox.ContainsPoint(touch.Location) ?? false)
                    //{
                    //    return false;
                    //}
                    return true;
                },
                OnTouchEnded = (_, __) =>
                  {
                    //                    Close();
                },
                
            };


            AddChild(layer, this.ZOrder);
            this.AddEventListener(eventListener,-100);
//            SetListenerPriority(eventListener, -50);
            CCLog.Log($"[OreOreLog]:[YesNoDialog]{this.ZOrder}");

            //var lll = new CCLabel("たいとるもどる", "Arial", 30)
            //{
            //    Position = new CCPoint(400, 500),
            //};

            //AddChild(lll);

            //var ls = new CCEventListenerTouchOneByOne()
            //{
            //    OnTouchBegan = (_, __) => true,
            //    OnTouchEnded = (_, __) =>
            //    {
            //        if (lll.BoundingBox.ContainsPoint(_.Location))
            //        {
            //            var scene = new CCScene(GameView);
            //            scene.AddChild(new Layers.Loading(new TitleAssetsLoader()));
            //            Director.ReplaceScene(scene);
            //            Close();
            //        }
            //    },

            //};
//            AddEventListener(ls, -1000);
        }
    }
}
