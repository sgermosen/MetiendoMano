using System;
using CocosSharp;

namespace MonsterSmashing.Shared
{
	public class Monster : CCNode
	{
		public string MonsterSprite { get; set; }
		public string SplashSprite { get; set; }
		public int Movement { get; set; }
		public float MinVelocity { get; set; }
		public float MaxVelocity { get; set; }
		public int KillMethod { get; set; }

		public Monster()
		{
		}
	}
}
