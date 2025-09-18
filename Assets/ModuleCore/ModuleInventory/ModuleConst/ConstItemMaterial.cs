using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 材料 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/材料")]
public class ConstItemMaterial : ConstItem {
	/// <summary> 最大堆叠数 </summary>
	public int maxStack = 99;

	public override DataItem To() {
		DataItemMaterial item = new DataItemMaterial();
		item.name = name;
		item.sprite = sprite;
		item.maxStack = maxStack;
		return item;
	}
}
