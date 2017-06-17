using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class Player : IFighter
    {
        public CCNode Node { get; }

        private CCPoint EndPosition { get; set; }
        private bool IsTap { get; set; } = false;

        public bool IsFired { get; }

        public bool IsEnable { get; private set; } = true;

        public virtual CCEventListener TouchListener
        {
            get
            {
                return new CCEventListenerTouchAllAtOnce()
                {
                    OnTouchesBegan = (touch, __) =>
                    {
                        IsTap = true;
                        EndPosition =touch[0].Location;
                    },
                    OnTouchesMoved = (touch, __) =>
                    {
                        EndPosition = touch[0].Location;
                    },
                    OnTouchesEnded = (touch, __) =>
                    {
                        IsTap = false;
                        EndPosition = touch[0].Location;
                    }
                };
            }
        }

        private float _speed = 5;

        PlayerFire _fire = new PlayerFire();

        public Player()
        {
            Node = new CCSprite("Images/Character/Me")
            {
                Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Height - (GlobalGameData.Window_Height * (10.0f / 13.0f)))
            };
        }


        public virtual void Update()
        {
            if (!IsEnable) return;
            if (IsTap)
            {
                var abs = EndPosition - Node.Position;
                var angle = Math.Atan2(abs.Y, abs.X);

                var new_X =(int)( Node.Position.X + _speed * Math.Cos(angle));
                var new_Y =(int)( Node.Position.Y + _speed * Math.Sin(angle));
                var new_position = (new CCPoint((float)new_X, (float)new_Y));

                Node.Position = new_position;
            }

            var x = Node.PositionX;
            var y = Node.PositionY;
            if (x < 0)
            {
                Node.PositionX = 0;
            }
            else if (x > GlobalGameData.Window_Width)
            {
                Node.PositionX = GlobalGameData.Window_Width;
            }
            if (y < 0)
            {
                Node.PositionY = 0;
            }
            else if (y > GlobalGameData.Window_Height)
            {
                Node.PositionY = GlobalGameData.Window_Height;
            }
        }


        public IList<IBullet<IShot>> Fire(IList<IBullet<IShot>>list)
        {
            var bullets=_fire.CreateBullets(new CCPoint(Node.Position.X, Node.Position.Y + 15),list).ToList();
            foreach (var bullet in bullets)
            {
                bullet.Node.ZOrder = 5;
            }
            return bullets;
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
        
    }
}
