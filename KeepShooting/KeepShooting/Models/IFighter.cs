using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace KeepShooting.Models
{
    public interface IFighter : IMover,IFireable
    {
        //private CharacterData _char = null;

        //public CCSprite Image { get; }

        //public bool IsFired { get => _char.IsFired; }


        //public Fighter(CharacterData character)
        //{
        //    _char = character;
        //    Image = _char.Image;
        //}
        
        //public virtual void Update()
        //{
        //    _char.Update();
        //}

        //public virtual IList<IBullet<IShot>> Fire()
        //{
        //    return _char.CreateBullets().ToList();
        //}
     
    }
}
