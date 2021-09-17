using System;
using CocosSharp;
using System.Collections.Generic;
using System.Collections;
using CocosDenshion;

namespace MonsterSmashing.Shared
{
	public class MonsterRun : CCLayerColor
	{
		private readonly List<Monster> _monsters;
		private readonly List<CCNode> _monstersOnScreen;
		private float _appScale;
		private int _lives = 3;
		private List<CCNode> _hearths;

		public MonsterRun()
		{
			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesBegan = OnTouchesEnded;

			AddEventListener (touchListener, this);

			_monsters = new List<Monster>();
			_monstersOnScreen = new List<CCNode>();
			_hearths = new List<CCNode>();
		}

		protected override void AddedToScene()
		{
			base.AddedToScene();

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;
			//			CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("backgroundSound.mp3");

			var winSize = Window.WindowSizeInPixels;
			var backgroundImage = new CCSprite("WoodRetroApple_iPad_HomeScreen.jpg");
			backgroundImage.Position = new CCPoint(winSize.Width / 2, winSize.Height / 2);
			_appScale = winSize.Height / backgroundImage.ContentSize.Height;
			backgroundImage.ScaleX = _appScale;
			backgroundImage.ScaleY = _appScale;
			AddChild(backgroundImage, -2);

			var m1 = new Monster();
			m1.Tag = 0;
			m1.MonsterSprite = "monsterGreen.png";
			m1.SplashSprite = "splashMonsterGreen.png";
			m1.MinVelocity = 2.0f;
			m1.MaxVelocity = 8.0f;
			m1.Movement = 1;
			m1.KillMethod = 1;
			_monsters.Add(m1);

			var m2 = new Monster();
			m2.Tag = 1;
			m2.MonsterSprite = "monsterBlue.png";
			m2.SplashSprite = "splashMonsterBlue.png";
			m2.MinVelocity = 2.0f;
			m2.MaxVelocity = 8.0f;
			m2.Movement = 1;
			m2.KillMethod = 2;
			_monsters.Add(m2);

			var m3 = new Monster();
			m3.Tag = 2;
			m3.MonsterSprite = "monsterRed.png";
			m3.SplashSprite = "splashMonsterRed.png";
			m3.MinVelocity = 3.0f;
			m3.MaxVelocity = 6.0f;
			m3.Movement = 2;
			m3.KillMethod = 1;
			_monsters.Add(m3);

			this.Schedule(AddMonster, 1.0f);

			for(var i = 0; i < _lives; i++)
			{
				var hearth = new CCSprite("hearth.png");
				_hearths.Add(hearth);
				hearth.Position = new CCPoint((i+1)*50, winSize.Height - 50);
				AddChild(hearth);
			}
		}

		private void AddMonster(float time)
		{
			var selectedMonster = new Random().Next() % _monsters.Count;

			var monster = _monsters[selectedMonster];
			var m = monster.Movement;

			//!IMPORTANT -- Every Sprite in Screen must be an new CCSprite! Each Sprite can only be one time on screen
			var spriteMonster = new CCSprite(monster.MonsterSprite);
			spriteMonster.Scale = _appScale;
			spriteMonster.Tag = monster.Tag;

			//BLOCK 1 - Determine where to spawn the monster along the Y axis
			var winSize = Window.WindowSizeInPixels;
			var minX = (spriteMonster.ContentSize.Width / 2);
			var maxX = winSize.Width - spriteMonster.ContentSize.Width / 2;
			var rangeX = maxX - minX;
			var actualY = (new Random().Next() % rangeX) + minX;

			//BLOCK 2 - Determine speed of the monster
			var minDuration = monster.MinVelocity;
			var maxDuration = monster.MaxVelocity;
			var rangeDuration = maxDuration - minDuration;
			var actualDuration = (new Random().Next() % rangeDuration) + minDuration;

			if(m == 1)
			{
				spriteMonster.Position = new CCPoint(actualY, winSize.Height + spriteMonster.ContentSize.Height/2);
				AddChild(spriteMonster);

				var actionMove = new CCMoveTo(actualDuration, new CCPoint(actualY, -spriteMonster.ContentSize.Height/2));
				var actionMoveComplete = new CCCallFuncN (node => 
					{
						_monstersOnScreen.Remove(node);
						node.RemoveFromParent();

						_lives--;
						var index = _hearths.Count - 1;
						RemoveChild(_hearths[index]);
						_hearths.RemoveAt(index);
						if(_lives == 0)
						{
							Window.DefaultDirector.ReplaceScene(GameStartLayer.GameStartLayerScene(Window));
						}
					});

				spriteMonster.RunActions(actionMove, actionMoveComplete);
				_monstersOnScreen.Add(spriteMonster);
			}
			else if(m == 2)
			{
				spriteMonster.Position = new CCPoint(actualY, winSize.Height + spriteMonster.ContentSize.Height/2);
				AddChild(spriteMonster);

				var actionMoveComplete = new CCCallFuncN (node => 
					{
						_monstersOnScreen.Remove(node);
						node.RemoveFromParent();

						_lives--;
						var index = _hearths.Count - 1;
						RemoveChild(_hearths[index]);
						_hearths.RemoveAt(index);
						if(_lives == 0)
						{
							Window.DefaultDirector.ReplaceScene(GameStartLayer.GameStartLayerScene(Window));
						}
					});

				var bezierList = new List<CCFiniteTimeAction>();
				var bezier = new CCBezierConfig();
				var splitDuration = actualDuration / 6.0f;
				CCBezierTo bezierAction;

				for(int i = 0; i < 6; i++)
				{
					if(i % 2 == 0)
					{
						bezier.ControlPoint1 = new CCPoint(actualY + 100, winSize.Height - (100 + (i * 200)));
						bezier.ControlPoint2 = new CCPoint(actualY + 100, winSize.Height - (100 + (i * 200)));
						bezier.EndPosition = new CCPoint(actualY, winSize.Height - (200 + (i * 200)));
						bezierAction = new CCBezierTo(splitDuration, bezier);
					}
					else
					{
						bezier.ControlPoint1 = new CCPoint(actualY - 100, winSize.Height - (100 + (i * 200)));
						bezier.ControlPoint2 = new CCPoint(actualY - 100, winSize.Height - (100 + (i * 200)));
						bezier.EndPosition = new CCPoint(actualY, winSize.Height - (200 + (i * 200)));
						bezierAction = new CCBezierTo(splitDuration, bezier);
					}

					bezierList.Add(bezierAction);
				}

				bezierList.Add(actionMoveComplete);

				var seq = new CCSequence(bezierList.ToArray());
				spriteMonster.RunAction(seq);

				_monstersOnScreen.Add(spriteMonster);
			}
		}

		void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
		{
			var monstersToDelete = new List<CCNode>();

			var touch = touches[0];
			var touchLocation = Layer.ScreenToWorldspace(touch.LocationOnScreen);

			foreach(var monster in _monstersOnScreen)
			{
				if(monster.BoundingBox.ContainsPoint(touchLocation))
				{
					monstersToDelete.Add(monster);

					var m = _monsters[monster.Tag];
					var splashPool = new CCSprite(m.SplashSprite);

					if(m.KillMethod == 1)
					{
						splashPool.Position = monster.Position;
						AddChild(splashPool);

						var fade = new CCFadeOut(3.0f);
						var remove = new CCCallFuncN (RemoveSprite);
						var sequencia = new CCSequence(fade, remove);
						CCSimpleAudioEngine.SharedEngine.EffectsVolume = 0.7f;
						CCSimpleAudioEngine.SharedEngine.PlayEffect("Sounds/SplatEffect");
						splashPool.RunAction(sequencia);
					}
					else if(m.KillMethod == 2)
					{
						CCSimpleAudioEngine.SharedEngine.EffectsVolume = 1.0f;
						CCSimpleAudioEngine.SharedEngine.PlayEffect("Sounds/pew-pew-lei");

						splashPool.Position = monster.Position;
						AddChild(splashPool);

						var fade = new CCFadeOut(3.0f);
						var particleEmitter = new CCCallFuncND(StartExplosion, monster);
						var sequencia = new CCSequence(particleEmitter, fade);
						CCSimpleAudioEngine.SharedEngine.EffectsVolume = 0.7f;
						CCSimpleAudioEngine.SharedEngine.PlayEffect("Sounds/SplatEffect");
						splashPool.RunAction(sequencia);
					}
					break;
				}
			}

			foreach(var monster in monstersToDelete)
			{
				monster.StopAllActions();
				_monstersOnScreen.Remove(monster);
				RemoveChild(monster);
			}
		}

		void RemoveSprite(CCNode node)
		{
			RemoveChild(node);
		}

		void StartExplosion(CCNode monster, object arg2)
		{
			var particleExplosion = new CCParticleExplosion(monster.Position) 
			{ 
				TotalParticles = 809,
				Texture = CCTextureCache.SharedTextureCache.AddImage("textureRed.png"),
				Life = 0.0f,
				LifeVar = 0.708f,
				StartSize = 40,
				StartSizeVar = 38,
				EndSize = 14,
				EndSizeVar = 0,
				Angle = 360,
				AngleVar = 360,
				Speed = 243,
				SpeedVar = 1
			};

			particleExplosion.Gravity = new CCPoint(1.15f, 1.58f);
			particleExplosion.StartColor = new CCColor4F(0.89f, 0.56f, 0.36f, 1.0f);
			particleExplosion.EndColor = new CCColor4F(1.0f,0.0f,0.0f,1.0f);

			AddChild(particleExplosion);
			particleExplosion.ResetSystem();
		}

		public static CCScene GameScene (CCWindow mainWindow)
		{
			var scene = new CCScene (mainWindow);
			var layer = new MonsterRun();

			scene.AddChild(layer);

			return scene;
		}
	}
}
