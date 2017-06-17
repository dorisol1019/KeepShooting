using PCLStorage;
using System.Threading.Tasks;

namespace KeepShooting.Models
{
    public class GameAssetsLoader : BaseAssetsLoader
    {
        PlayData _playData = null;

        string _playDataPath = null;
        GameAssets _gameAssets = new GameAssets();
        public GameAssetsLoader(GameAssets gameAssets,string playDataPath = null)
            : base(gameAssets)
        {
            _playDataPath = playDataPath;
            _gameAssets = gameAssets;
        }

        public override async Task Load()
        {
            await base.Load();
            if (_playDataPath == null) return;
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            var folder = await rootFolder.CreateFolderAsync("PlayData", CreationCollisionOption.OpenIfExists);
            var file = await folder.GetFileAsync(_playDataPath);


            string json = await file.ReadAllTextAsync();

            json = Encode.Deflate.Decode(json);

            _playData = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayData>(json);

        }

        public override void Navigate(ISceneChanger sceneChanger)
        {
            string bgm = "";
            if(_playData==null)
            {
                bgm = _gameAssets.BackgroundMusicPath;
            }
            else
            {
                bgm = _playData.BGMFileName;
            }
            Layers.Game game = new Layers.Game(bgm,_playData);
            sceneChanger.ChangeScene(game);
        }
    }
}
