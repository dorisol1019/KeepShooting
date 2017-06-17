using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class NormalEnemySkin:EnemySkin
    {
        public NormalEnemySkin()
        {
            ImgPath = "Images/Character/ball";
            EnemyBulletsCreater = new NormalEnemyFire();
            Type = MoverType.NormalEnemy;
            Score = 3;
        }
    }
}
