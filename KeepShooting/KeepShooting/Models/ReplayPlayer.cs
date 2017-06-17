using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class ReplayPlayer : Player
    {
        Queue<CCPoint> _positionQueue = null;

        public ReplayPlayer(IEnumerable<Point> positions)
        {
            _positionQueue = new Queue<CCPoint>(positions.Select(e => e.ToCCPoint()));
        }

        public override void Update()
        {
            //base.Update();
            if (!IsEnable) return;
            Node.Position = _positionQueue.Dequeue();
        }

        public override CCEventListener TouchListener
        {
            get
            {
                return new CCEventListenerTouchOneByOne()
                {
                    OnTouchBegan = (_, _001_) =>
                    {
                        var spr = new CCSprite("Images/System/DoNotTouch")
                        {
                            Position = new CCPoint(Node.Parent.ContentSize.Center),
                            Tag = 100100,
                        };
                        Node.Parent.AddChild(spr);
                        return true;
                    },
                    OnTouchEnded = (_, __) =>
                      {
                          Node.Parent.RemoveChildByTag(100100);
                      }

                };
            }
        }
    }
}
