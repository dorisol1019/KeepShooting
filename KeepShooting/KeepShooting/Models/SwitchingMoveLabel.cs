using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public class SwitchingMoveLabel : MoveLabel
    {
        public SwitchingMoveLabel(CCLabel label, IShot move) : base(label, move)
        {
        }
        
        public void MoveStart()
        {
            IsEnable = true;
        }

        public void MoveEnd()
        {
            IsEnable = false;
        }

        public override void Update()
        {
            if(IsEnable)
            {
                Node.Position = _move.UpdatePosition(Node.Position);
            }
        }
    }
}
