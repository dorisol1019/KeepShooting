using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public class ReplayEnemy : Enemy
    {
        public ReplayEnemy(CCNode node,int positionX,MoverType type) : base(new EnemySkinFactory_Assign(type), node)
        {
            Node.PositionX = positionX;
        }
    }
}
