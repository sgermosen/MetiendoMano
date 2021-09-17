using System;
using CocosSharp;
using CocosDenshion;

namespace MonsterSmashing.Shared
{
	public class MonsterSmashingAppDelegate : CCApplicationDelegate
	{
		public MonsterSmashingAppDelegate()
		{
		}

		public override void ApplicationDidFinishLaunching (CCApplication application, CCWindow mainWindow)
		{
			application.PreferMultiSampling = false;
			application.ContentRootDirectory = "Content";

			try
			{
				CCSimpleAudioEngine.SharedEngine.PreloadEffect("Sounds/SplatEffect");
				CCSimpleAudioEngine.SharedEngine.PreloadEffect("Sounds/pew-pew-lei");
				CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic("Sounds/backgroundSound", true);
				CCSimpleAudioEngine.SharedEngine.BackgroundMusicVolume = 0.9f;	
				CCSimpleAudioEngine.SharedEngine.EffectsVolume = 0.7f;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}


			var winSize = mainWindow.WindowSizeInPixels;
			mainWindow.SetDesignResolutionSize(winSize.Width, winSize.Height, CCSceneResolutionPolicy.ExactFit);
//			CCScene.SetDefaultDesignResolution(winSize.Width, winSize.Height, CCSceneResolutionPolicy.ExactFit);

			// TODO: Set this up when we have a Game Layer
			CCScene scene = GameStartLayer.GameStartLayerScene(mainWindow);
			mainWindow.RunWithScene (scene);
		}

		public override void ApplicationDidEnterBackground (CCApplication application)
		{
			// stop all of the animation actions that are running.
			application.Paused = true;

			// if you use SimpleAudioEngine, your music must be paused
			CCSimpleAudioEngine.SharedEngine.PauseBackgroundMusic ();
		}

		public override void ApplicationWillEnterForeground (CCApplication application)
		{
			application.Paused = false;

			// if you use SimpleAudioEngine, your background music track must resume here. 
			CCSimpleAudioEngine.SharedEngine.ResumeBackgroundMusic ();
		}
	}
}
