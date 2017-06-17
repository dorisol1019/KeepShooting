using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KeepShooting.Models;

namespace KeepShooting.Layers
{
    public class TestLayer2 : BaseLayer
    {
        CCNode backButton = null;


        public TestLayer2() : base(new CCColor4B(0x11, 0x11, 0x11, 0x80))
        {

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            backButton = new CCLabel("もどる", "arial", 40)
            {
                Position = new CCPoint(GlobalGameData.Window_Center_X, 430)
            };
            AddChild(backButton);

            var t = new CCEventListenerTouchOneByOne();
            {
                t.IsSwallowTouches = true;
                t.OnTouchBegan = (_, __) => true;
                t.OnTouchEnded = (touch, __) =>
                  {
                      if (backButton.BoundingBox.ContainsPoint(touch.Location))
                      {
                          RemoveEventListener(t);
                          RemoveFromParent(true);
                      }
                  };
            };

            AddEventListener(t, -1);
        }



    }
}
