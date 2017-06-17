using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeepShooting.Models;
using PCLStorage;

namespace KeepShooting.Layers
{
    public class Game : BaseLayer
    {
        Models.Player player = null;
        List<Enemy> enemys = null;
        List<IBullet<IShot>> playerBullets = null;
        List<IBullet<IShot>> enemyBullets = null;
        BrokenMover[] brokenMovers = null;

        int Score = 0;

        string randomBGM = "";

        List<Point> playerPoint = null;
        Queue<int> enemyPopPointX = null;
        Queue<MoverType> enemyTypes = null;

        readonly GameMode _mode;

        PlayData _playData = null;

        public Game(string bgm, PlayData playData = null)
        {
            randomBGM = bgm;
            if (playData == null)
            {
                _mode = GameMode.Normal;
                _playData = new PlayData();
            }
            else
            {
                _mode = GameMode.Replay;
                _playData = playData;
            }

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var backGround = new CCSprite("Images/BackGround/space", null)
            {
                Position = ContentSize.Center
            };

            AddChild(backGround);

            if (_mode == GameMode.Replay)
            {                
                playerPoint = _playData.PlayerPositions;
                enemyPopPointX = new Queue<int>(_playData.EnemyPopPoints_X);
                enemyTypes = new Queue<MoverType>(_playData.MoverTypes);
                var modeLabel = new CCLabel("リプレイモード", "arial", 30)
                {
                    Color = new CCColor3B(255, 100, 100),
                    Position = new CCPoint(380, GlobalGameData.Window_Height - 20)
                };
                AddChild(modeLabel);
            }


            player = _mode == GameMode.Normal ? new Player() : new ReplayPlayer(playerPoint);
            AddChild(player.Node);

            enemys = new List<Enemy>(100);
            playerBullets = new List<IBullet<IShot>>(50);
            enemyBullets = new List<IBullet<IShot>>(100);
            
            if (_mode == GameMode.Normal)
            {
                playerPoint = new List<Point>(10000);
                enemyPopPointX = new Queue<int>(100);
                enemyTypes = new Queue<MoverType>(100);
            }


            brokenMovers = new BrokenMover[2];
            brokenMovers[0] = new BrokenMover(new CCPoint(0, 650), (new NormalEnemySkin()).ImgPath, MoverType.NormalEnemy);
            brokenMovers[1] = new BrokenMover(new CCPoint(0, 620), (new StrongEnemySkin()).ImgPath, MoverType.StrongEnemy);

            AddChild(brokenMovers[0].Image);
            AddChild(brokenMovers[1].Image);
            AddChild(brokenMovers[0].Nodes[1]);
            AddChild(brokenMovers[1].Nodes[1]);


            var count3 = new CCSprite("Images/System/Count3");
            var count2 = new CCSprite("Images/System/Count2");
            var count1 = new CCSprite("Images/System/Count1");
            var countGo = new CCSprite("Images/System/CountGo") { Tag = 0x1919 };
            var counts = new[] { count3, count2, count1, countGo };

            var countAction = new CCMoveTo(1, ContentSize.Center);

            var countsQueue = new Queue<CCNode>(counts);
            var remove = new CCRemoveSelf();
            foreach (var count in counts)
            {
                count.Position = new CCPoint(1000, GlobalGameData.Window_Center_Y);
                AddChild(count);
            }

            void s(float f)
            {
                if (!countsQueue.Any())
                {
                    CCAudioEngine.SharedEngine.PlayBackgroundMusic(randomBGM, true);
                    Unschedule(s);
                    Schedule(RunGameLogic);
                    Schedule((_) =>
                    {

                        if (_mode == GameMode.Normal)
                        {
                            Enemy enemy = new Enemy(new EnemySkinFactory(), this);
                            enemyPopPointX.Enqueue(enemy.InitXPoint);
                            enemyTypes.Enqueue(enemy.Type);
                            AddEnemy(enemy);                            
                        }
                        else if (_mode == GameMode.Replay)
                        {
                            if (!enemyPopPointX.Any() || !enemyTypes.Any()) return;
                            AddEnemy(new ReplayEnemy(this, enemyPopPointX.Dequeue(), enemyTypes.Dequeue()));
                        }
                    },
                    1.0f);

                    Schedule((_) =>
                    {
                        if (!player.IsEnable) return;
                        AddBullets(playerBullets, player.Fire(playerBullets));
                    }, (1.0f / 6.0f));

                    Schedule(_ =>
                    {
                        if (!player.IsEnable) return;
                        Score++;
                    }, 1.0f);


                    AddEventListener(player.TouchListener, this);

                }
                else
                {
                    var count = countsQueue.Dequeue();
                    count.RunActions(countAction, remove);
                    if (count.Tag == 0x1919)
                    {
                        CCAudioEngine.SharedEngine.PlayEffect("SE/Countdown01-6");
                    }
                    else
                    {
                        CCAudioEngine.SharedEngine.PlayEffect("SE/Countdown01-5");
                    }
                }
            };
            Schedule(s, 1, 4, 1);

            var button = new CCSprite("Images/System/gear")
            {
                Position = new CCPoint(30, 30),
            };

            var buttontouch = new CCEventListenerTouchOneByOne()
            {
                IsSwallowTouches = true,
                OnTouchBegan = (touch, _) =>
                 {
                     if (button.BoundingBox.ContainsPoint(touch.Location))
                     {
                         isbuttontouch = true;
                         return true;
                     }
                     return false;
                 },
                OnTouchMoved = (touch, _) =>
                  {
                      if (!isbuttontouch) return;
                      isbuttontouch = false;
                      if (button.BoundingBox.ContainsPoint(touch.Location))
                      {
                          isbuttontouch = true;
                      }
                  },
                OnTouchEnded = (touch, _) =>
                  {
                      if (!isbuttontouch) return;
                      if (button.BoundingBox.ContainsPoint(touch.Location))
                      {
                          this.Pause();
                          //var dialog = new YesNoDialog("停止なう", "再開しますか",
                          //    ___ =>
                          //    {
                          //        var _dialog = GetChildByTag(YesNoDialog.TAG) as YesNoDialog;
                          //        _dialog.Close();
                          //        RemoveChild(_dialog);
                          //        this.Resume();

                          //    }, null);
                          //AddChild(dialog, 1, YesNoDialog.TAG);
                          var pauseMenu = new ModalMenu()
                          {
                              Position = ContentSize.Center,
                          };
                          var retryGame = new CCMenuItemLabel(new CCLabel("ゲームをやりなおす", "Arials", 50),
                              A =>
                              {
                                  ChangeScene(new Game(randomBGM));

                                  var dialog = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                  dialog.Close();
                              });
                          var goRanking = new CCMenuItemLabel(new CCLabel("ランキングにもどる", "Arial", 50),
                              A =>
                              {
                                  ChangeScene(new Loading(new RankingAssetsLoader()));
                                  var dialog = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                  dialog.Close();
                              });
                          var goTitle = new CCMenuItemLabel(new CCLabel("タイトルにもどる", "Arial", 50),
                              A =>
                              {
                                  ChangeScene(new Loading(new TitleAssetsLoader()));
                                  var dialog = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                  dialog.Close();
                              });
                          var resumeGame = new CCMenuItemLabel(new CCLabel("ゲームをつづける", "Arial", 50),
                              A =>
                              {
                                  this.Resume();
                                  var dialog = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                  dialog.Close();
                                  RemoveChild(dialog);

                              });

                          var menuItems = new[] { retryGame, goTitle, resumeGame };
                          

                          foreach (var menuItem in menuItems)
                          {
                              pauseMenu.AddChild(menuItem);
                          }

                          pauseMenu.AlignItemsVertically(20);

                          var modalMenuDialog = new ModalMenuDialog(pauseMenu);

                          AddChild(modalMenuDialog, 1, ModalMenuDialog.TAG);
                          
                      }
                  }
            };

            AddChild(button);
            AddEventListener(buttontouch);

        }
        bool isbuttontouch = false;

        private void AddEnemy(Enemy enemy)
        {

            for (int i = 0; i < enemys.Count; i++)
            {
                if (!enemys[i].IsEnable)
                {
                    RemoveChild(enemys[i].Node);
                    enemys[i] = enemy;
                    AddChild(enemy.Node);
                    return;
                }
            }

            enemys.Add(enemy);
            AddChild(enemy.Node);
        }


        private void AddBullets(List<IBullet<IShot>> charachterBullets, IEnumerable<IBullet<IShot>> bullets)
        {
            {
                charachterBullets.AddRange(bullets);
                foreach (var bullet in bullets)
                {
                    AddChild(bullet.Node);
                }
            }
        }

        private void RunGameLogic(float frameTimeInSeconds)
        {
            player.Update();
            if (_mode == GameMode.Normal)
            {
                if (player.IsEnable)
                {
                    playerPoint.Add(player.Node.Position.ToPoint());
                }
            }

            if (player.IsFired)
            {
                AddBullets(playerBullets, player.Fire(playerBullets));
            }

            foreach (var enemy in enemys)
            {
                enemy.Update();
                if (enemy.IsFired)
                {
                    AddBullets(enemyBullets, enemy.Fire(enemyBullets));
                }
            }

            foreach (var playerBullet in playerBullets)
            {
                playerBullet.Update();
            }

            foreach (var enemyBullet in enemyBullets)
            {
                enemyBullet.Update();
            }


            //ChackHit
            foreach (var enemy in enemys.Where(e => e.IsEnable))
            {
                foreach (var playerBullet in playerBullets.Where(e => e.IsEnable))
                {
                    if (CheckHitMoverAndMover(playerBullet, enemy))
                    {
                        enemy.Break();
                        playerBullet.Break();
                        var brokenMover = brokenMovers.First(e => e.Type == enemy.Type);
                        brokenMover.Increment();
                        Score += enemy.Score;
                        break;
                    }
                }
            }

            foreach (var enemy in enemys.Where(e => e.IsEnable))
            {
                if (CheckHitMoverAndMover(player, enemy))
                {
                    BreakPlayer();
                    break;
                }
            }

            foreach (var enemyBullet in enemyBullets.Where(e => e.IsEnable))
            {
                if (CheckHitMoverAndMover(player, enemyBullet))
                {
                    BreakPlayer();
                    break;
                }
            }

        }

        private bool CheckHitMoverAndMover(IMover mover1, IMover mover2)
        {
            if (!mover1.IsEnable) return false;
            if (!mover2.IsEnable) return false;
            return mover1.Intersects(mover2);
        }

        private void BreakPlayer()
        {
            CCAudioEngine.SharedEngine.EffectsVolume = 1.0f;
            player.Break();
            CCAudioEngine.SharedEngine.PlayEffect("SE/explosion");
            ScheduleOnce(_ =>
           {
               CCAudioEngine.SharedEngine.StopBackgroundMusic();

               if (_mode == GameMode.Normal)
               {
                   PlayData data = new PlayData(randomBGM, playerPoint, enemyPopPointX, enemyTypes);
                   data.Score = Score;

                   var newScene_ = new CCScene(GameView);
                   var gameLayer_ = new Loading(new RankingAssetsLoader(data));
                   newScene_.AddChild(gameLayer_);

                   // ゲーム画面へシーン切り替え
                   this.Director.ReplaceScene(newScene_);
                   return;
               }


               var newScene = new CCScene(GameView);
               var gameLayer = new Loading((new RankingAssetsLoader()));
               newScene.AddChild(gameLayer);


               // ゲーム画面へシーン切り替え
               this.Director.ReplaceScene(newScene);
           }, 1);
        }
    }
}
