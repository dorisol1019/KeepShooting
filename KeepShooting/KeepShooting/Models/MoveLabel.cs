using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public class MoveLabel : IMover
    {
        public CCNode Node { get; set; }

        public string Text { get => (Node as CCLabel).Text; set => (Node as CCLabel).Text = value; }

        public bool IsEnable { get; protected set; } = true;

        protected IShot _move=null;

        public MoveLabel(CCLabel label,IShot move)
        {
            Node = label;
            _move = move;
        }

        public void Break()
        {
            throw new NotImplementedException();
        }

        public bool Intersects(IMover mover)
        {
            throw new NotImplementedException();
        }

        public virtual void Update()
        {
            Node.Position = _move.UpdatePosition(Node.Position);
        }
    }
}
