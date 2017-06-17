using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{


    public class ModalMenuDialog : ModalLayer
    {
        ModalMenu _modalMenu;

        public const int TAG = -10102221;

        public ModalMenuDialog(ModalMenu modalMenu)
        {
            _modalMenu = modalMenu;
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            layer.AddChild(_modalMenu/*,layer.ZOrder+1*/);
        }

    }
}
