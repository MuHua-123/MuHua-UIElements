using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/装备")]
public class ConstEquipment : ConstItem {
	/// <summary> 装备类型 </summary>
	public string type;

	public override DataItem To() {
		DataEquipment item = new DataEquipment();
		item.name = name;
		item.sprite = sprite;
		item.type = type;
		return item;
	}
}
