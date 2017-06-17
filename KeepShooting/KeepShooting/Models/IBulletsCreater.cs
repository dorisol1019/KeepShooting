using CocosSharp;
using System.Collections.Generic;

namespace KeepShooting.Models
{
    public interface IBulletsCreater
    {
        IList<IBullet<IShot>> CreateBullets(CCPoint position,IList<IBullet<IShot>>list);
    }
}
