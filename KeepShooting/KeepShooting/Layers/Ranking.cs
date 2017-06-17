using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KeepShooting.Models;

using PCLStorage;
using System.IO;

namespace KeepShooting.Layers
{
    public class Ranking : BaseLayer
    {
        private RankingData _rankingData;
        bool isSwiped = false;



        public Ranking(RankingData ranking)  //base(new CCColor4B(0xcc, 0xcc, 0xcc))
        {
            _rankingData = ranking;

            Init();
        }

        void Init()
        {
            var gesture = CCEventListenerGesture.Create();
            gesture.SetSwipeThreshouldDistance(80);
            gesture.OnSwipeEnded = (swipe) =>
              {
                  isSwiped = false;
                  switch (swipe)
                  {
                      case CCEventListenerGesture.SwipeDirection.NONE:
                          break;
                      case CCEventListenerGesture.SwipeDirection.UP:
                      case CCEventListenerGesture.SwipeDirection.DOWN:
                      case CCEventListenerGesture.SwipeDirection.LEFT: //ふぉーるするー
                      case CCEventListenerGesture.SwipeDirection.RIGHT:
                          CCLog.Log($"[OreOreLog]:{nameof(TestLayer)}{this.ZOrder}");

                          var goGame = new CCMenuItemLabel(new CCLabel("ゲームであそぶ", "Arial", 40),
                                _ =>
                                {
                                    ChangeScene(new Loading(new GameAssetsLoader(new GameAssets())));
                                    var dig = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                    dig.Close();
                                });


                          var goTitle = new CCMenuItemLabel(new CCLabel("タイトルにもどる", "Arial", 40),
                              _ =>
                              {
                                  ChangeScene(new Loading(new TitleAssetsLoader()));
                                  var dig = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                  dig.Close();
                              });

                          var cancel = new CCMenuItemLabel(new CCLabel("キャンセル", "Arial", 40),
                              _ =>
                              {
                                  var dig = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                  dig.Close();
                                  RemoveChild(dig);
                                  isSwiped = false;
                              });
                          var sns_share = new CCMenuItemLabel(new CCLabel("ランキングのシェア", "Arial", 40),
                              _ =>
                              {
                                  var dig = GetChildByTag(ModalMenuDialog.TAG) as ModalMenuDialog;
                                  dig.Close();
                                  RemoveChild(dig);

                                  void Share(CCLayer layer)
                                  {
                                      CCRenderTexture rt = new CCRenderTexture(VisibleBoundsWorldspace.Size, VisibleBoundsWorldspace.Size, CCSurfaceFormat.Color, CCDepthFormat.Depth24Stencil8);
                                      rt.BeginWithClear(CCColor4B.Black);
                                      layer.Visit();
                                      rt.End();
                                      rt.Sprite.Position = VisibleBoundsWorldspace.Center;
                                      CCRenderCommand shareCommand = new CCCustomCommand(
                                          () => {
                                              using (MemoryStream ms = new MemoryStream())
                                              {
                                                  //rt.Texture.SaveAsPng(ms, (int)layer.VisibleBoundsWorldspace.Size.Width, (int)layer.VisibleBoundsWorldspace.Size.Height);
                                                  //ShareControl.ShareImage(ms);
                                                  rt.SaveToStream(ms,CCImageFormat.Png);
                                                  var _share = Xamarin.Forms.DependencyService.Get<IShareSNS>();

                                                  string text = "";
                                                  if(_rankingData.IsRankinCurrentPlayData)
                                                  {
                                                      int score = _rankingData.Ranks[_rankingData.RankinIndex].Score;
                                                      if (_rankingData.RankinIndex==0)
                                                      {
                                                          text = $"わたしのスコア:{score}点" +
                                                          $"\n" +
                                                          $"ハイスコア更新しました！";
                                                      }
                                                      else
                                                      {
                                                          text = $"わたしのスコア:{score}点" +
                                                          $"\n" +
                                                          $"ランキング更新しました！";
                                                      }
                                                  }
                                                  else
                                                  {
                                                      text = "今のランキングです！";
                                                  }

                                                  string hashTag = "#撃ち続けろ";

                                                  text = text + "\n" + hashTag;
                                                  _share.Post(text, ms);
                                              }
                                          });
                                      Renderer.AddCommand(shareCommand);
                                  }
                                  Share(this);
                                  isSwiped = false;
                                  return;

                              });

                          var menuItems = new[] { goGame, goTitle, sns_share, cancel };

                          var menu = new ModalMenu(menuItems)
                          {
                              Position = new CCPoint(GlobalGameData.Window_Center_X, GlobalGameData.Window_Center_Y - 30),
                          };

                          menu.AlignItemsVertically(30);

                          var dialog = new ModalMenuDialog(menu);

                          AddChild(dialog, 1, ModalMenuDialog.TAG);


                          isSwiped = true;
                          break;
                      default:
                          break;
                  }

              };
            AddEventListener(gesture, this.Point.First());

            var touch = new CCEventListenerTouchOneByOne();
            touch.OnTouchBegan = (_, __) =>
            {
                foreach (var label in _rankPointsLabel)
                {
                    if (label.Label.BoundingBoxTransformedToWorld.ContainsPoint(_.Location))
                    {
                        selectedMenuItem = label;
                        label.Selected = true;
                        return true;
                    }
                }
                return false;
            };
            touch.OnTouchMoved = (_, __) =>
              {
                  CCMenuItem current = null;
                  foreach (var label in _rankPointsLabel)
                  {
                      if (label.Label.BoundingBoxTransformedToWorld.ContainsPoint(_.Location))
                      {
                          current = label; break;
                      }
                  }
                  if (current != selectedMenuItem)
                  {
                      if (selectedMenuItem != null)
                      {
                          selectedMenuItem.Selected = false;
                      }

                      if (current != null)
                      {
                          current.Selected = true;
                      }
                      selectedMenuItem = current;
                  }
              };
            touch.OnTouchEnded = /*EventListener_TouchEnded*/
                (_, __) =>
                {
                    if (selectedMenuItem != null)
                    {
                        selectedMenuItem.Selected = false;
                        selectedMenuItem.Activate();
                    }
                };
            AddEventListener(touch, this);

            if (_rankingData.CurrentPlayData != null)
            {
                var currentScoreLabel = new CCLabel($"あなたのスコア:{_rankingData.CurrentPlayData.Score}点", "arial", 20)
                {
                    Position = new CCPoint(20, GlobalGameData.Window_Height - 20),
                    AnchorPoint = CCPoint.AnchorMiddleLeft
                };
                AddChild(currentScoreLabel);
                if (_rankingData.IsRankinCurrentPlayData)
                {
                    var omedetoLabel = new CCLabel("ランクインおめでとう！", "arial", 20)
                    {
                        Position = new CCPoint(380, GlobalGameData.Window_Height - 20)
                    };
                    AddChild(omedetoLabel);
                }
            }
            systemLabel = new CCLabel("スワイプ:メニューを開く　 スコアを選ぶ:リプレイ", "arial", 20)
            {
                Position = new CCPoint(20, 20),
                AnchorPoint = CCPoint.AnchorMiddleLeft,
                Visible = false
            };
            AddChild(systemLabel);
        }
        CCLabel systemLabel = null;
        CCMenuItem selectedMenuItem = null;
        bool isNext = false;
        private void EventListener_TouchEnded(CCTouch arg1, CCEvent arg2)
        {
            if (isNext) return;
            if (isSwiped) { isSwiped = false; return; }
            for (int i = 0; i < _rankPoints.Length; i++)
            {
                var point = _rankPoints[i];
                if (point.Node.BoundingBox.ContainsPoint(arg1.Location))
                {
                    var filename = _rankingData.Ranks[i].RankedFileName;
                    if (filename == "") continue;
                    isNext = true;
                    var dialog = new YesNoDialog($"{i + 1}位  ", $"このゲームをリプレイしますか？",
                        _ =>
                        {
                            var _dialog = (YesNoDialog)GetChildByTag(YesNoDialog.TAG);

                            _dialog.Close();


                            ChangeScene(new Loading(new GameAssetsLoader(new GameAssets(), filename)));
                        },
                        _ =>
                        {
                            var _dialog = (YesNoDialog)GetChildByTag(YesNoDialog.TAG);

                            _dialog.Close();

                            RemoveChild(_dialog);

                            isNext = false;
                        });
                    AddChild(dialog, 1, YesNoDialog.TAG);
                    break;
                }
            }
        }

        CCLabel[] _ranks = new CCLabel[10];

        SwitchingMoveLabel[] _rankPoints = new SwitchingMoveLabel[10];

        CCMenuItemLabel[] _rankPointsLabel = new CCMenuItemLabel[10];

        CCLabel[] Point = new CCLabel[10];

        protected override void AddedToScene()
        {
            base.AddedToScene();
            CCAudioEngine.SharedEngine.PlayBackgroundMusic("Music/Ranking/new_castle-the-order", true);
            for (int i = 0; i < _ranks.Length; i++)
            {
                _ranks[i] = new CCLabel($"{10 - i}位", "Arial", 30)
                {
                    Position = new CCPoint(60, i * (50 + 10) + 50)
                };
                AddChild(_ranks[i]);
            }

            for (int i = 0; i < Point.Length; i++)
            {
                Point[i] = new CCLabel("点", "Arial", 30)
                {
                    Position = new CCPoint(450 + 15, i * (50 + 10) + 50)
                };
                AddChild(Point[i]);

            }

            for (int i = 0; i < _rankPoints.Length; i++)
            {
                int score = _rankingData.Ranks[i].Score;

                CCLabel cCLabel = new CCLabel($"{score}", "Arial", 50)
                {
                    //Position = new CCPoint(i * 50 + GlobalGameData.Window_Width + 500, GlobalGameData.Window_Height - (i * 60 + 70)),
                    //                    AnchorPoint = new CCPoint(1, 0.5f),
                    Color = CCColor3B.White,
                    Position = new CCPoint(0, 0)
                };
                if (_rankingData.IsRankinCurrentPlayData)
                {
                    if (_rankingData.RankinIndex == i)
                    {
                        cCLabel.Color = new CCColor3B(100, 255, 255);
                    }
                }

                //_rankPoints[i] = new SwitchingMoveLabel(cCLabel, new LinerShot(new CCVector2(-1, 0), 12));
                //_rankPoints[i].MoveStart();
                //                AddChild(_rankPoints[i].Node);
                string txt = $"{i + 1}位のゲームをリプレイしますか？";
                int i_ = i;
                var filename = _rankingData.Ranks[i_].RankedFileName;
                _rankPointsLabel[i] = new CCMenuItemLabel((cCLabel), filename != "" ?
                    (_) =>
                    {
                        if (isNext) return;
                        if (isSwiped) { isSwiped = false; return; }
                        var dialog = new YesNoDialog($"{i_ + 1}位 {score}点", "このゲームをリプレイしますか？",
                        _1 =>
                        {
                            var _dialog = (YesNoDialog)GetChildByTag(YesNoDialog.TAG);

                            _dialog.Close();




                            ChangeScene(new Loading(new GameAssetsLoader(new GameAssets(), filename)));
                        },
                        _1 =>
                        {
                            var _dialog = (YesNoDialog)GetChildByTag(YesNoDialog.TAG);

                            _dialog.Close();

                            RemoveChild(_dialog);

                            isNext = false;
                        });
                        AddChild(dialog, 1, YesNoDialog.TAG);
                        isNext = true;
                    }
                : null as Action<object>
                    )
                {
                    Position = new CCPoint(i * 50 + GlobalGameData.Window_Width + 500, GlobalGameData.Window_Height - (i * 60 + 60)),

                    AnchorPoint = CCPoint.AnchorMiddleRight,

                };

                AddChild(_rankPointsLabel[i]);

            }

            Schedule(RunGameLogic);
        }


        private void RunGameLogic(float obj)
        {
            //foreach (var rankPoint in _rankPoints)
            //{
            //    rankPoint.Update();

            //    if (rankPoint.Node.Position.X < 420)
            //    {
            //        rankPoint.Node.PositionX = 420;
            //        rankPoint.MoveEnd();
            //    }
            //}
            bool[] isAllRankPointLabelX420 = new bool[_rankPointsLabel.Length];
            for (int i = 0; i < _rankPointsLabel.Length; i++)
            {
                isAllRankPointLabelX420[i] = false;
                if ((int)_rankPointsLabel[i].PositionX == 420)
                {
                    isAllRankPointLabelX420[i] = true;
                    continue;
                }
                _rankPointsLabel[i].PositionX -= 12;
                if (_rankPointsLabel[i].PositionX < 420)
                {
                    _rankPointsLabel[i].PositionX = 420;
                }
            }
            if (systemLabel.Visible) return;
            if (isAllRankPointLabelX420.All(e => e))
            {
                systemLabel.Visible = true;
            }
        }
    }
}
