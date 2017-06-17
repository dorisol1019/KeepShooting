using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public abstract class EnemySkin
    {
        public string ImgPath { get; protected set; }
        public IBulletsCreater EnemyBulletsCreater { get; protected set; }
        public MoverType Type { get; protected set; }
        public int Score { get; protected set; }
    }
}
