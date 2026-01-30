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
		public TUnit Model => unit;

		public virtual void Bind(TUnit unitModel)
		{
			this.unit = unitModel;
			SubscribeEvents();
			RefreshView();
		}
		
		public void SubscribeEvents()
		{
			if (unit != null)
			{
				unit.HpChanged -= UnitOnHpChanged;
				unit.Died -= UnitOnDied;
				
				unit.HpChanged += UnitOnHpChanged;
				unit.Died += UnitOnDied;
			}
		}
		
		private void UnsubscribeEvents()
		{
			if (unit != null)
			{
				unit.HpChanged -= UnitOnHpChanged;
				unit.Died -= UnitOnDied;
			}
		}
		
		protected virtual void OnEnable()
		{
				SubscribeEvents();
		}

		protected virtual void OnDisable()
		{
			UnsubscribeEvents();
		}

		//子类必须实现这个方法，用来初始化显示
		protected abstract void RefreshView();

		protected virtual void UnitOnHpChanged(Unit source, int damage)
		{ }


		protected virtual void UnitOnDied(Unit source)
		{
		}
	}
}