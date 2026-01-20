using Combat;
using Units.Core;
using UnityEngine;

namespace Units.Player
{
	public class PlayerUnit : Unit
	{
		// 压力上限,这个暂时不确定，后面可能还需要更改是否需要压力值
		[field: Header("Resources (BG3)")]
		[field: SerializeField]
		public int MaxSanity { get; protected set; } = 100;

		[field: SerializeField] public int MaxRp { get; protected set; } // RP-职业资源
		[field: SerializeField] public int MaxAp { get; protected set; } // AP-标准动作
		[field: SerializeField] public int MAxBp { get; protected set; } // BP-附赠动作
		
		public int CurrentSanity { get; protected set; }
		public int CurrentRp { get; protected set; }
		public int CurrentAp { get; protected set; }
		public int CurrentBp { get; protected set; }


		public void ResetTurnResources()
		{
			CurrentAp = MaxAp;
			CurrentBp = MaxHp;
			// Rp 只应该在短修时恢复
		}

		public void TakeDamage(int damage, DamageType type)
		{
			if (IsDead) return;

			float resistance = 0.0f;
			if (ResistanceDict.ContainsKey(type))
			{
				resistance = ResistanceDict[type];
			}

			int finalDamage = Mathf.RoundToInt(damage * (1.0f - resistance));

			CurrentHp -= finalDamage;
			CurrentHp = CurrentHp <= 0 ? 0 : CurrentHp;

			// TODO: 推送血量改变事件

			// 死亡检测
			if (CurrentHp <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			IsDead = true;
			CurrentHp = 0;
			// TODO: 触发死亡事件

			Debug.Log($"[{CharacterName}] <color=red>已阵亡。</color>");
		}

		/// <summary>
		/// Debug 输出伤害信息，只负责输出不负责计算
		/// </summary>
		private void LogDamageInfo(DamageType type, int rawDice, float resistance, int finalDice)
		{
			string suffix = resistance switch
			{
				< 0f => " <color=orange>(弱点!)</color>",
				>= 1f => " <color=grey>(免疫)</color>",
				> 0f => " (抵抗)",
				_ => ""
			};

			Debug.Log($"[{CharacterName}] 受到 [{type}] 伤害: 原始{rawDice} -> 最终{finalDice}{suffix}");
		}

		
		
	}
}