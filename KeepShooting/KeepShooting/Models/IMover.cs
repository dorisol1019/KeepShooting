using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public interface IMover
    {
        CCNode Node { get; }
        bool IsEnable { get; }
        void Break();
        
        void Update();

        bool Intersects(IMover mover);
    }
}
