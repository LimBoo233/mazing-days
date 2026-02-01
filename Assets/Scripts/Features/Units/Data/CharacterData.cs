using System;
using System.Collections.Generic;
using Modules.Combat.Data;
using Modules.Combat.Data.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Units.Data
{
	/// <summary>
	/// 存放角色数据
	/// </summary>
	[Serializable]
	public class CharacterData : IUnitData
	{
		public Vector3 LogicalPosition { get; set; }
	}
}