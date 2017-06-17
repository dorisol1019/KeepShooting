using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class RankingAssets : IAssets
    {
        public string[] ImagePaths => new string[0];

        public string[] SoundEffectPaths =>
            new string[0]
            //{ "" }
            ;
        public string BackgroundMusicPath => "Music/Ranking/new_castle-the-order";
    }
}
