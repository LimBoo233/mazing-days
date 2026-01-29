using System;
using GameSystemEnum;

namespace Features.Units.Core
{
	[Serializable]
	public class EnemyUnit : Unit
	{
		public override void InitializeStats()
		{
			base.InitializeStats();
			FactionType = FactionType.Enemy;
		}
	}
}