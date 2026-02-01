using System;
using Features.Units.Data;
using GameSystemEnum;

namespace Features.Units.Core
{
	[Serializable]
	public class EnemyUnit : Unit<EnemyData>
	{
		public override void InitializeStats()
		{
			base.InitializeStats();
			Data.FactionType = FactionType.Enemy;
		}
	}
}