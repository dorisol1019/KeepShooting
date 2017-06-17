using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public class PlayerBullet<TShot> : IBullet<TShot> where TShot:IShot
    {
        public CCNode Node { get; }

        public TShot _shot { get; }

        public bool IsEnable { get; private set; } = true;
        //private TShot _shot;
        public PlayerBullet(CCPoint initPosition, TShot t)
        {
            Node = new CCSprite("Images/Bullet/PlayerBullet");
            Node.Position = initPosition;
            _shot = t;
        }

        public void Update()
        {
            if (!IsEnable) return;
            Node.Position = _shot.UpdatePosition(Node.Position);


            var x = Node.PositionX;
            var y = Node.PositionY;
            if (x < (0 - 60) || x > (GlobalGameData.Window_Width + 60))
            {
                IsEnable = false;
                Node.Visible = false;
            }
            else if (y < (0 - 60) || y > (GlobalGameData.Window_Height + 60))
            {
                IsEnable = false;
                Node.Visible = false;

            }

        }

        public bool Intersects(IMover mover)
        {
            return Collision2D.CheckHitCircleAndCircle(this, mover);
        }

        public void Break()
        {
            IsEnable = false;
            Node.Visible = false;
        }

        public void Enable(CCPoint potision)
        {
            Node.Position = potision;
            IsEnable = true;
            Node.Visible = true;
        }

        public void SetVec(CCVector2 vec)
        {
            throw new NotImplementedException();
        }
    }
}
