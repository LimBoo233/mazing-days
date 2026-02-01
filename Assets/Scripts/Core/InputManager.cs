using Features.Units.Core;
using Features.Units.Data;
using Modules.Combat.View;
using Modules.Exploration.View;
using UnityEngine;


namespace Core
{
	public static class InputConstants
	{
		public const string Horizontal = nameof(Horizontal);
		public const string Vertical = nameof(Vertical);
		public const string Jump = nameof(Jump);
	}
	
	public class InputManager
	{
		public void EarlyUpdate()
		{
			
		}
		
		public Vector2 GetMoveDirection()
		{
			float horizontal = Input.GetAxisRaw(InputConstants.Horizontal);
			float vertical = Input.GetAxisRaw(InputConstants.Vertical);

			return new Vector2(horizontal, vertical).normalized;
		}
		
		public bool IsJumpPressed() => Input.GetButtonDown(InputConstants.Jump);

		public bool IsTrySelected() => Input.GetMouseButtonDown(0);
		
		public bool IsCancelSelected() => Input.GetMouseButtonDown(1);
		
		public Transform TrySelectedObject()
		{
			if (Camera.main == null) return null;
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 1000f))
			{
				return hitInfo.transform;
			}

			return null;
		}
		
		public Unit TrySelectCombatUnit()
		{
			Transform selectedTransform = TrySelectedObject();
			if (selectedTransform == null) return null;
			
			var combatView = selectedTransform.GetComponentInParent<UnitCombatView<Unit>>();
			return combatView?.Model;
		}
		
		public UnitData TrySelectedWorldCharacter()
		{
			Transform selectedTransform = TrySelectedObject();
			if (selectedTransform == null) return null;
			
			var overworldView = selectedTransform.GetComponentInParent<OverworldView<UnitData>>();
			return overworldView?.UnitData;
		}
		

	}
}