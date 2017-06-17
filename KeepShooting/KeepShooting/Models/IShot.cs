using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public interface IShot
    {
        CCPoint UpdatePosition(CCPoint position);
        void SetVec(CCVector2 vec);
    }
}
