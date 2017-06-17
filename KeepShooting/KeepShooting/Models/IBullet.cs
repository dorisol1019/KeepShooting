using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public interface IBullet<out TShot>:IMover where TShot:IShot
    {
        TShot _shot { get; }

        void Enable(CCPoint potision);
        void SetVec(CCVector2 vec);
    }
}
