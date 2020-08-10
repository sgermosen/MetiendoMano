using System;
using CocosSharp;
using CocosDenshion;

namespace MonsterSmashing.Shared
{
	public class GameStartLayer : CCLayerColor
	{
		public GameStartLayer()
		{
			//			var touchListener = new CCEventListenerTouchAllAtOnce ();
			//			touchListener.OnTouchesEnded = (touches, ccevent) => Window.DefaultDirector.ReplaceScene (GameLayer.GameScene (Window));
			//
			//			AddEventListener (touchListener, this);
			//
			//			Color = CCColor3B.Black;
			//			Opacity = 255;
		}

		CCMenuItemImage _soundOn, _soundOff;
		private float _appScale;

		protected override void AddedToScene()
		{
			base.AddedToScene();

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;

			var winSize = Window.WindowSizeInPixels;
			var backgroundImage = new CCSprite("WoodRetroApple_iPad_HomeScreen.jpg");
			backgroundImage.Position = new CCPoint(winSize.Width / 2, winSize.Height / 2);
			_appScale = winSize.Height / backgroundImage.ContentSize.Height;
			backgroundImage.ScaleX = _appScale;
			backgroundImage.ScaleY = _appScale;
			AddChild(backgroundImage, -2);

			var monsters = new CCSprite("backgroundMonsters2.png");
			monsters.Position = new CCPoint(winSize.Width / 2, winSize.Height / 2);
			monsters.ScaleX = _appScale;
			monsters.ScaleY = _appScale;
			AddChild(monsters, -1);

			var logo = new CCSprite("MonsterSmashing.png");
			logo.Position = new CCPoint(winSize.Width/2, winSize.Height * 0.7f);
			logo.Scale = _appScale;
			AddChild(logo);

			var startGameButtonImage = new CCMenuItemImage("play.png", "playSelected.png", obj =>
				{
					var transition = new CCTransitionFlipAngular(0.5f, MonsterRun.GameScene(Window), CCTransitionOrientation.DownOver);
					Window.DefaultDirector.ReplaceScene(transition);
				});
			startGameButtonImage.Scale = _appScale;

			_soundOn = new CCMenuItemImage("soundOn.png", "soundOnSelected.png");
			_soundOff = new CCMenuItemImage("soundOff.png", "soundOffSelected.png");
			var toggleItem = new CCMenuItemToggle(obj => {}, _soundOn, _soundOff);
			toggleItem.Scale = _appScale;

			var menu = new CCMenu(startGameButtonImage, toggleItem);
			menu.Position = new CCPoint(winSize.Width * 0.5f, winSize.Height * 0.4f);
			menu.AlignItemsVertically(15);
			AddChild(menu);
		}

		public static CCScene GameStartLayerScene (CCWindow mainWindow)
		{
			var scene = new CCScene(mainWindow);
			var layer = new GameStartLayer();

			scene.AddChild (layer);

			return scene;
		}
	}
}
