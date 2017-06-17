using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public class StrongEnemyFire : IBulletsCreater
    {
        CCVector2[] vecs= new CCVector2[5];
        public StrongEnemyFire()
        {
            for (int i = 0; i < vecs.Length; i++)
            {
                double radian = -(60 + ((120 - 60) / (vecs.Length - 1) * i)) * (Math.PI / 180);
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
                    if (itemi == vecs.Length)
                    {
                        return Array.Empty<IBullet<IShot>>();
                    }
                }
            }
            var l = new List<IBullet<IShot>>();
            for (int i = itemi; i < vecs.Length; i++)
            {
                var bullet = new EnemyBullet<LinerShot>(position, new LinerShot(vecs[i], 5));
                //list.Add(bullet);
                l.Add(bullet);
            }
            return l;
        }

    }
}
