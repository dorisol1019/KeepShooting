using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class TitleAssets : IAssets
    {
        public string[] ImagePaths { get; } 
            = new string[] 
            {
                "Images/Title/title_old"
            };

        public string[] SoundEffectPaths => Array.Empty<string>();

        public string BackgroundMusicPath => "Music/Title/bgm_maoudamashii_8bit24";
    }
}
