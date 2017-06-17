using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public interface IFireable
    {
        bool IsFired { get; }
        //IEnumerable<IBullet<IShot>> CreateBullets(CCPoint position);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IBullet<IShot>> Fire(IList<IBullet<IShot>> list);
        
    }
}
