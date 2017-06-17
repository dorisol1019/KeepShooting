using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public class PlayerFire:IBulletsCreater
    {
        CCVector2 vec;
        public PlayerFire()
        {
            vec = new CCVector2(0, 1);
        }

        public IList<IBullet<IShot>> CreateBullets(CCPoint position,IList<IBullet<IShot>>list)
        {
            foreach (var _bullet in list)
            {
                if(!_bullet.IsEnable)
                {
                    _bullet.Enable(position);
                    return Array.Empty<IBullet<IShot>>();
                }
            }


            var list2 = new List<IBullet<IShot>>();
            var bullet = new PlayerBullet<LinerShot>(position,new LinerShot(vec, 5));

            //list.Add(bullet);
            list2.Add(bullet);

            return list2;
        }               
    }
}
