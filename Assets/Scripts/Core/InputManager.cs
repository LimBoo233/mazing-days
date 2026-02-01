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
		public Vector2 GetMoveDirection()
		{
			float horizontal = Input.GetAxisRaw(InputConstants.Horizontal);
			float vertical = Input.GetAxisRaw(InputConstants.Vertical);

			return new Vector2(horizontal, vertical).normalized;
		}
		
		public bool IsJumpPressed() => Input.GetButtonDown(InputConstants.Jump);
		
		
	}
}