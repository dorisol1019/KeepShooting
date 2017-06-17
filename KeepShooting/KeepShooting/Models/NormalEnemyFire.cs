using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public class NormalEnemyFire : IBulletsCreater
    {
        CCVector2[] vecs = new CCVector2[3];
        public NormalEnemyFire()
        {
            for (int i = 0; i < 3; i++)
            {
                double radian = -(60 + ((120 - 60) / (3 - 1) * i)) * (Math.PI / 180);
                vecs[i] = new CCVector2((float)Math.Cos(radian), (float)Math.Sin(radian));
            }
        }

        public IList<IBullet<IShot>> CreateBullets(CCPoint position, IList<IBullet<IShot>> list)
        {

            int itemi = 0;
            foreach (var item in list)
            {
                if (!item.IsEnable)
                {
                    item.Enable(position);
                    item.SetVec(vecs[itemi++]);
                    if (itemi == 3)
                    {
                        return Array.Empty<IBullet<IShot>>();
                    }
                }
            }

            var list2 = new List<IBullet<IShot>>();
            for (int i = itemi; i < 3; i++)
            {
                var bullet = new EnemyBullet<LinerShot>(position, new LinerShot(vecs[i], 5));
                //list.Add(bullet);
                list2.Add(bullet);
            }
            return list2;
        }
    }
}
