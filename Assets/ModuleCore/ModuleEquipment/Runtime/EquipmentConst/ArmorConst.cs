using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 护甲 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/护甲")]
public class ArmorConst : EquipmentConst {
	/// <summary> 护甲类型 </summary>
	public ArmorType type;

	// public override InventoryItem To() {
	// 	Equipment item = new Equipment();
	// 	item.name = name;
	// 	item.sprite = sprite;
	// 	item.type = type.ToString();
	// 	return item;
	// }
}
