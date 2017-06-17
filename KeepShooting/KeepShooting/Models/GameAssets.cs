using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class GameAssets : IAssets
    {
        public string[] ImagePaths { get; }
            = new string[]
            {
                "Images/Character/Me",
                "Images/BackGround/space.png"
            };

        public string[] SoundEffectPaths { get; }
            = new string[]
            {
                "SE/explosion"
            };

        public string BackgroundMusicPath {
            get
            {
                int index = CCRandom.Next(_bgmPaths.Length);
                return _bgmPaths[index];
            }
        }

        private string[] _bgmPaths = new string[]
            {
                "Music/Game/bgm_maoudamashii_8bit18",
                "Music/Game/bgm_maoudamashii_fantasy03",
                "Music/Game/bgm_maoudamashii_fantasy04",
                "Music/Game/bgm_maoudamashii_fantasy11",
                "Music/Game/bgm_maoudamashii_fantasy12",
                "Music/Game/bgm_maoudamashii_fantasy15"
            };

    }
}
