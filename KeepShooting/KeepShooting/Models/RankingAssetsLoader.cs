using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KeepShooting.Layers;
using PCLStorage;

namespace KeepShooting.Models
{
    public class RankingAssetsLoader : BaseAssetsLoader
    {
        RankingData _rankingData = null;
        PlayData _playData = null;

        public RankingAssetsLoader(PlayData playData = null)
            : base(new RankingAssets())
        {
            _playData = playData;
        }

        public override async Task Load()
        {
            await base.Load();
            _rankingData = await IORanking.LoadAsync();
            if (_playData == null)
            {
                IORanking.SaveAsync(_rankingData).Wait();
                return;
            }
            for (int i = 0; i < _rankingData.Ranks.Count; i++)
            {
                _rankingData.CurrentPlayData = _playData;
                var rankData = _rankingData.Ranks[i];
                if (_playData.Score > rankData.Score)
                {
                    //string fileName = System.Guid.NewGuid().ToString();
                    string fileName = DateTime.Now.ToString("yyyyMMddhhmiss");
                    string ext = ".drf";
                    _rankingData.Ranks.Insert(i, new RankData()
                    {
                        Score = _playData.Score,
                        RankedFileName = fileName + ext,
                    });

                    _rankingData.IsRankinCurrentPlayData = true;
                    _rankingData.RankinIndex = i;

                    IORanking.SaveAsync(_rankingData).Wait();

                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(_playData, Newtonsoft.Json.Formatting.None);
                    json = Encode.Deflate.Encode(json);
                    Task.Run(async () =>
                    {
                        IFolder rootFolder = FileSystem.Current.LocalStorage;
                        var folder = await rootFolder.CreateFolderAsync("PlayData", CreationCollisionOption.OpenIfExists);
                        var file = await folder.CreateFileAsync(fileName + ext, CreationCollisionOption.ReplaceExisting);
                        await file.WriteAllTextAsync(json);

                    }).Wait();
                    break;
                }
            }
            Task.Run(async () =>
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                var folder = await rootFolder.CreateFolderAsync("PlayData", CreationCollisionOption.OpenIfExists);

                var files = await folder.GetFilesAsync();

                var rankedFileNames = _rankingData.Ranks.Take(10).Select(f => f.RankedFileName).ToArray();

                foreach (var file in files)
                {
                    if (!rankedFileNames.Contains(file.Name))
                    {
                        await file.DeleteAsync();
                    }
                }

            }).Wait();
        }

        public override void Navigate(ISceneChanger sceneChanger)
        {
            sceneChanger.ChangeScene(new Ranking(_rankingData));
        }
    }
}
