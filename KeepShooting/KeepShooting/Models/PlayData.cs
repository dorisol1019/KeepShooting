using CocosSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class PlayData
    {
        public const string Curent_Version = "1.0.0.0";

        public PlayData()
        {
            Version = Curent_Version;
            PlayerPositions = new List<Point>();
            BGMFileName = "";
            EnemyPopPoints_X =new List<int>();
            MoverTypes = new List<MoverType>();
        }

        public PlayData( string bgmFileName, IEnumerable<Point> playerPoint, IEnumerable<int> enemyPopPointX, IEnumerable<MoverType> moverTypes, string version = Curent_Version)
        {
            Version = version;
            PlayerPositions = playerPoint.ToList();
            BGMFileName = bgmFileName;
            EnemyPopPoints_X = enemyPopPointX.ToList();
            MoverTypes = moverTypes.ToList();
        }

        //        int mePlessKeyTotal;
        //        string mePlessKey;


        /// <summary>
        /// リプレイデータのバージョン
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; } = 0;

        [JsonProperty("PlayerPositions")]
        public List<Point> PlayerPositions { get; set; }

        ///        [JsonProperty("PlayerPositions")]
        //        public int PlayerPointLength { get => PlayerPositions.Count; }

        //        string shotFlag;

        ///        public int EnemyTotal { get => EnemyPopPointX.Count; }

        [JsonProperty("moverTypes")]
        public List<MoverType> MoverTypes { get; set; }

        [JsonProperty("enemyPopPoints_X")]
        public List<int> EnemyPopPoints_X { get; set; }


        [JsonProperty("bgmFileName")]
        public string BGMFileName { get; set; }
    }
}
