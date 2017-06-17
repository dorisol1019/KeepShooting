using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class IORanking
    {
        public static async Task<RankingData> LoadAsync()
        {
            var json = "";
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync("Ranking", CreationCollisionOption.OpenIfExists);
            var exists = await folder.CheckExistsAsync("ranking.drf");

            if (exists == (ExistenceCheckResult.NotFound))
            {
                return RankingData.Zero();
            }
            else if (exists == (ExistenceCheckResult.FileExists))
            {
                var file = await folder.GetFileAsync("ranking.drf");

                json = await file.ReadAllTextAsync();

            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RankingData>(json);
        }

        public static async Task SaveAsync(RankingData ranking)
        {
            var new_ranks = ranking.Ranks.Take(10).ToList();

            ranking.Ranks = new_ranks;

            var data = Newtonsoft.Json.JsonConvert.SerializeObject(ranking,Newtonsoft.Json.Formatting.None);


            IFolder rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync("Ranking", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync("ranking.drf", CreationCollisionOption.ReplaceExisting);

            await file.WriteAllTextAsync(data);
        }
    }
}
