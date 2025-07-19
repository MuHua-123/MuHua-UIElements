using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 附加 - 数据
/// </summary>
public class DataAddition {
	/// <summary> 护甲等级 </summary>
	public int armorClass = 0;

	/// <summary> 添加属性 </summary>
	public void Add(DataAddition value) {
		armorClass += value.armorClass;
	}
}
