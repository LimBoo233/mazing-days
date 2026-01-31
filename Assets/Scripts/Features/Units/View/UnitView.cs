using Features.UI;
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
		[SerializeField] private UnitUI unitUI;
		public TUnit Model => unit;

		public virtual void Bind(TUnit unitModel)
		{
			this.unit = unitModel;

			if (unitUI != null)
			{
				unitUI.Initialize(unitModel);
			}
			
			SubscribeEvents();
			RefreshView();
		}
		
		private void SubscribeEvents()
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
		protected virtual void RefreshView()
		{
			
		}

		//可以负责播放血量变更的动画
		protected virtual void UnitOnHpChanged(Unit source, int damage)
		{ }

		//播放死亡动画
		protected virtual void UnitOnDied(Unit source)
		{
		}
	}
}