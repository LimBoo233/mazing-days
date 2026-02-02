using System;
using Core;
using Features.Units.Core;
using Features.Units.Data;
using UnityEngine;

namespace Modules.Exploration.View
{
	/// <summary>
	/// 用于玩家角色的地图视图
	/// </summary>
	public class PlayerOverworldView: OverworldView<PlayerUnit>
	{
		private ExplorationModule _explorationModule;

		public override void Bind(PlayerUnit unit)
		{
			base.Bind(unit);
			SyncPosition();
			_explorationModule = unit.ExplorationModule;
		}
		
		protected void Update()
		{
			if (_explorationModule == null)
			{
				Debug.LogWarning("PlayerOverworldView: _explorationModule 为 null，无法更新");
				return;
			}
			
			var moveDir = GameManager.Input.GetMoveDirection();
			if (moveDir != Vector2.zero)
			{
				_explorationModule.Move(moveDir, Time.deltaTime);
				SyncPosition();
			}
		}
		
		private void SyncPosition()
		{
			if (_explorationModule == null) return;
			transform.position = _explorationModule.LogicalPosition;
		}
		
	}
}