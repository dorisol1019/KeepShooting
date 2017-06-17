using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class StrongEnemySkin:EnemySkin
    {
        public StrongEnemySkin()
        {
            ImgPath = "Images/Character/ball1";
            EnemyBulletsCreater = new StrongEnemyFire();
            Type = MoverType.StrongEnemy;
            Score = 6;
        }
    }
}
