using System;
using Features.Units.Data;
using UnityEngine;

namespace Modules.Exploration
{
	// 可交互接口
	public interface IInteractable
	{
		void Interact(ExplorationModule explorationModule);
	}

	/// <summary>
	/// 处理类似“偷窃”、“决斗”等地图指令逻辑。仅探索时生效
	/// </summary>
	[Serializable]
	public class ExplorationModule
	{
		public Vector3 LogicalPosition
		{
			get => _characterData.LogicalPosition;
			set => _characterData.LogicalPosition = value;
		}

		private CharacterData _characterData;
		private int _moveSpeed = 1;

		public void Bind(CharacterData characterData)
		{
			_characterData = characterData;
		}

		public void Move(Vector2 direction, int speed, float deltaTime)
		{
			Vector3 worldDir = new Vector3(direction.x, 0, direction.y);
			LogicalPosition += worldDir.normalized * (speed * deltaTime);
		}
		
		public void Move(Vector2 direction, float deltaTime) => Move(direction, _moveSpeed, deltaTime);

		/// <summary>
		/// 与特定事件交互
		/// </summary>
		public void Interact(IInteractable interactable)
		{
		}
		
		
	}
}