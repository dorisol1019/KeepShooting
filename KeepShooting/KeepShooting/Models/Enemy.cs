using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class Enemy : IFighter
    {
//        static Random _rand = new Random();
        static int Rand_Range_0_WindowWidth { get => CCRandom.Next(30,GlobalGameData.Window_Width-30); }

        public CCNode Node { get; protected set; }

        public bool IsEnable { get; private set; } = true;

        public bool IsFired { get; private set; } = true;

        public MoverType Type { get=>_skin.Type; }

        protected EnemySkin _skin = null;

        public int InitXPoint { get; }

        public Enemy(IEnemySkinFactory factory, CCNode layer)
        {
            _skin = factory.Create();

            InitXPoint = Rand_Range_0_WindowWidth;

            Node = new CCSprite(_skin.ImgPath, null)
            {
                Position = new CCPoint(InitXPoint, 60 + 650)
            };
            layer.Schedule((_) => 
            {
                if (IsEnable)
                    IsFired = true;
            }, 1);
        }
        
        public int Score { get => _skin.Score; }




        public void Update()
        {
            if (!IsEnable) return;
            Node.Position = new CCPoint(Node.Position.X, Node.Position.Y - 4);

            var x = Node.PositionX;
            var y = Node.PositionY;
            if (x < (0 - 60) || x > (GlobalGameData.Window_Width + 60))
            {
                IsEnable = false;
                Node.Visible = false;
            }
            else if (y < (0 - 60) || y > (GlobalGameData.Window_Height + 60))
            {
                Node.Visible = false;
                IsEnable = false;
            }

        }

        public IList<IBullet<IShot>> Fire(IList<IBullet<IShot>> list)
        {
            if (!IsFired) return null;
            IsFired = false;
            return _skin.EnemyBulletsCreater.CreateBullets(new CCPoint(Node.PositionX, Node.PositionY - 30),list).ToList();
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
