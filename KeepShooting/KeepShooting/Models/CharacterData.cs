using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public abstract class CharacterData //: IMover//, IBulletsCreater
    {


        public virtual bool IsFired { get; }

        protected CCSprite _image = null;
        public virtual CCSprite Image { get => _image ?? throw new NullReferenceException(); }

        protected IFireable _fire=null;

        public CharacterData(IFireable fire)
        {
            _fire = fire;
        }

        public virtual void Update()
        {
            throw new NotImplementedException();
        }


        public abstract IList<IBullet<IShot>> CreateBullets();

    }
}
