using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocosSharp;

namespace KeepShooting.Models
{
    public interface IAssets
    {
        string[] ImagePaths { get; }
        
        string[] SoundEffectPaths { get; }

        string BackgroundMusicPath { get; }

    }
}
