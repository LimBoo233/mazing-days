using Features.Units.Core;
using UnityEngine;

namespace Features.Units.View
{
	/// <summary>
	/// 用于 Unit 的前端显示
	/// </summary>
	/// <typeparam name="TUnit"></typeparam>
	/// 
	public abstract class UnitView<TUnit> : MonoBehaviour where TUnit : Unit
	{
		// [SerializeField] private Sprite _characterIcon;

		[SerializeField] protected TUnit unit;
		
		
		protected virtual void OnEnable()
		{
			if (unit != null)
			{
				unit.HpChanged += UnitOnHpChanged;
				unit.Died += UnitOnDied;
			}
		}

		protected virtual void OnDisable()
		{
			if (unit != null)
			{
				unit.HpChanged -= UnitOnHpChanged;
				unit.Died -= UnitOnDied;
			} 
		}

		protected virtual void UnitOnHpChanged(Unit arg1, int arg2)
		{
		}


		protected virtual void UnitOnDied(Unit obj)
		{
		}
	}
}