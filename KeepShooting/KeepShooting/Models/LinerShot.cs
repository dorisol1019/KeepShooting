using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class LinerShot : IShot
    {

        CCVector2 _vec;
        float _speed;
        public LinerShot(CCVector2 vec, float speed)
        {
            _vec = vec;
            _speed = speed;
        }
        
        public CCPoint UpdatePosition(CCPoint position)
        {
            //            return new CCPoint(position.X, position.Y + _speed);
            var d = (CCPoint)(_vec * _speed);
            return position + d;
        }

        public void SetVec(CCVector2 vec)
        {
            _vec = vec;
        }
    }
}
