using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KeepShooting.Models;

using CocosSharp;

namespace KeepShooting.Layers
{
    public class Loading : BaseLayer
    {
        BaseAssetsLoader _assetsLoader = null;

        CCSprite nowLoading_image = null;

        public Loading(BaseAssetsLoader assetsLoder)
        {
            _assetsLoader = assetsLoder;
            //            _assets = assets;
            //            IObserver<int> obs; obs.
            //           IObservable<int> k;k.Subscribe();

        }


        protected override void AddedToScene()
        {
            Schedule(RunGameLogic);
            ScheduleOnce(async _ =>
            {
                await Task.Delay(500);
                //await Task.Run(() =>
                //{
                //    _assetsLoader.Load();
                //});
                try
                {
                    await Task.Run(async () =>
                    {
                        await _assetsLoader.Load();
                    });
                }
                catch (Exception e)
                {
                    CCLog.Log(e.Message);
                    throw;
                }
                //                canNavigate = true;
                ScheduleOnce(__ =>
                {
                    _assetsLoader.Navigate(this);
                }, 0.5f);
            }
            , 0);


            base.AddedToScene();
            nowLoading_image = new CCSprite("Images/System/NowLoading")
            {
                AnchorPoint = new CCPoint(0.5f, 0.5f),
                Position = new CCPoint(500 - 70, GlobalGameData.Window_Center_Y),
                Scale = 2f
            };
            AddChild(nowLoading_image);
        }

        private void RunGameLogic(float obj)
        {
            //CCBlendFunc c=new CCBlendFunc();

            nowLoading_image.PositionX -= 1.0f;
        }

    }
}
