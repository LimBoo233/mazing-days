using Combat;
using Features.Units.Core;
using UnityEngine;

namespace Features.Units.Player
{
	public class PlayerUnit : Unit
	{
		// 压力上限,这个暂时不确定，后面可能还需要更改是否需要压力值
		[field: Header("Resources (BG3)")]
		[field: SerializeField]
		public int MaxSanity { get; protected set; } = 100;

		[field: SerializeField] public int MaxRp { get; protected set; } // RP-职业资源
		[field: SerializeField] public int MaxAp { get; protected set; } // AP-标准动作
		[field: SerializeField] public int MaxBp { get; protected set; } // BP-附赠动作
		
		public int CurrentSanity { get; protected set; }
		public int CurrentRp { get; protected set; }
		public int CurrentAp { get; protected set; }
		public int CurrentBp { get; protected set; }
		
		public void ResetTurnResources()
		{
			CurrentAp = MaxAp;
			CurrentBp = MaxBp;
			// Rp 只应该在短修时恢复
		}

		public bool TryConsumeResources(int apCost,int bpCost,int rpCost)
		{
			if (CurrentAp < apCost || CurrentBp < bpCost || CurrentRp < rpCost) 
				return false;

			CurrentAp -= apCost;
			CurrentBp -= bpCost;
			CurrentRp -= rpCost;
			return true;
		}
		
		void Update()
		{
			// 按下空格键测试
			if (Input.GetKeyDown(KeyCode.Space))
			{
				// 测试扣 5 点火焰伤害
				TakeDamage(5, DamageType.Fire);
        
		
        
				Debug.Log("【测试】按下了空格键，尝试扣血");
			}
		}
		
	}
}