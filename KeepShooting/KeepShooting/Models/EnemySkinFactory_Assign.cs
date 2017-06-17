using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class EnemySkinFactory_Assign : IEnemySkinFactory
    {
        private MoverType _type;

        public EnemySkinFactory_Assign(MoverType type)
        {
            if (type == MoverType.Player) throw new NotImplementedException("違うぞおめー");
            _type = type;
        }

        public EnemySkin Create()
        {
            switch (_type)
            {
                case MoverType.NormalEnemy:
                    return new NormalEnemySkin();
                case MoverType.StrongEnemy:
                    return new StrongEnemySkin();
                default:
                    break;
            }
            throw new NotImplementedException("違うぞおめー");
        }
    }
}
