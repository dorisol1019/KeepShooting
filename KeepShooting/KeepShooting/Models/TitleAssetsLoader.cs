using KeepShooting.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class TitleAssetsLoader:BaseAssetsLoader
    {
        public TitleAssetsLoader()
            :base(new TitleAssets(),new Title())
        {

        }
    }
}
