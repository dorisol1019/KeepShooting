using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocosSharp;
using KeepShooting.Models;

namespace KeepShooting.Layers
{
    public class BaseLayer : CCLayerColor, ISceneChanger
    {
        bool _isChange = false;

        public BaseLayer(CCColor4B?color=null) :base(color)
        {

        }


        public void ChangeScene(CCLayer layer)
        {
            if (_isChange) return;
            if(CCAudioEngine.SharedEngine.BackgroundMusicPlaying)
            {
                CCAudioEngine.SharedEngine.StopBackgroundMusic();
            }

            _isChange = true;
            var newScene = new CCScene(GameView);

            newScene.AddChild(layer);

            // シーン切り替え時の効果を設定
            //CCTransitionScene cCTransitionScene = new CCTransitionScene(0, newScene);

            this.Director.ReplaceScene(newScene);
            

        }

        private void Replace(CCTransitionScene scene)
        {

            // ゲーム画面へシーン切り替え
            this.Director.ReplaceScene(scene);

            
        }
    }
}
