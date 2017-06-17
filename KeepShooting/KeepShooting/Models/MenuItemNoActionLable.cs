using System;

using CocosSharp;
namespace KeepShooting.Models
{
    public class MenuItemNoActionLable : CCMenuItemLabel
    {


        public MenuItemNoActionLable(CCLabel label, Action<object> target = null) : base(label,target)
        {

        }

        public override bool Selected { set  { } }
    }
}
