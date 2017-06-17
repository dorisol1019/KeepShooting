using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class EnemySkinFactory:IEnemySkinFactory
    {

        public virtual EnemySkin Create()
        {
            EnemySkin _skin = new NormalEnemySkin();
            if (CCRandom.Next(100) < 20)
            {
                _skin = new StrongEnemySkin();
            }
            return _skin;
        }
    }
}
