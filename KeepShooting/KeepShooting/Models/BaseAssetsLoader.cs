using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public abstract class BaseAssetsLoader : ILoader, INavigator
    {
        private IAssets _assets = null;
        private CCLayer _layer = null;

        public BaseAssetsLoader(IAssets assets, CCLayer layer = null)
        {
            _assets = assets;
            _layer = layer;
        }

        public virtual async Task Load()
        {
            //await Task.Run(() =>
            {
                foreach (var imagePath in _assets.ImagePaths)
                {
                    //PreloadTexture(imagePath);
                }
                PreloadBGM(_assets.BackgroundMusicPath);
                foreach (var path in _assets.SoundEffectPaths)
                {
                    PreloadSE(path);
                }
            }
            //);
        }

        public virtual void Navigate(ISceneChanger sceneChanger)
        {
            if (_layer == null)
            {
                throw new NullReferenceException("_layerがnullなためNavigateはできません");
            }
            sceneChanger.ChangeScene(_layer);
        }


        void PreloadTexture(string path)
        {
            if (CCTextureCache.SharedTextureCache.Contains(path)) return;
            using (var fs = CCFileUtils.GetFileStream(path+".xnb"))
            {
                new CCTexture2D(fs);
            }
        }

        void PreloadBGM(string path)
        {
            CCAudioEngine.SharedEngine.PreloadBackgroundMusic(path);
        }

        void PreloadSE(string path)
        {
            CCAudioEngine.SharedEngine.PreloadEffect(path);
        }

    }
}
