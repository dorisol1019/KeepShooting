using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace KeepShooting.Models
{
    public class RankData
    {
        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("rankedFileName")]
        public string RankedFileName { get; set; }
    }
    public class RankingData
    {
        public const string Current_Version = "1.0.0.0";

        [JsonProperty("version")]
        public string Version { get; set; } = Current_Version;
        
        
        [JsonProperty("ranks")]
        public List<RankData> Ranks { get; set; }

        [JsonIgnore]
        public bool IsRankinCurrentPlayData { get; set; } = false;

        [JsonIgnore]
        public int RankinIndex { get; set; } = -1;

        [JsonIgnore]
        public PlayData CurrentPlayData { get; set; } = null;

        public static RankingData Zero()
        {
            RankingData ranking = new RankingData()
            {
                Ranks = new List<RankData>(11)
            };
            for (int i = 0; i < 10; i++)
            {
                var rank = new RankData()
                {
                    Score = 0,
                    RankedFileName = ""
                };
                ranking.Ranks.Add(rank);
            }
            return ranking;
        }
    }
}
