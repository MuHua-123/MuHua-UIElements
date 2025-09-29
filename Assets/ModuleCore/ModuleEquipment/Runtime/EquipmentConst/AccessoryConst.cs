using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 饰品 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/饰品")]
public class AccessoryConst : EquipmentConst {
	/// <summary> 饰品类型 </summary>
	public AccessoryType type;

	public override InventoryItem To() {
		Equipment item = new Equipment();
		item.name = name;
		item.sprite = sprite;
		item.type = type.ToString();
		return item;
	}
}
