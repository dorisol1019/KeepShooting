using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class Collision2D
    {
        static public bool CheckHitCircleAndCircle(IMover mover1, IMover mover2)
        {
            var x1 = mover1.Node.PositionX;
            var y1 = mover1.Node.PositionY;
            var x2 = mover2.Node.PositionX;
            var y2 = mover2.Node.PositionY;

            var r1 = mover1.Node.ContentSize.Height / 2;
            var r2 = mover2.Node.ContentSize.Height / 2;

            if ((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1) <= (r1 + r2) * (r1 + r2))
            {
                return true;
            }


            return false;
        }
    }
}
