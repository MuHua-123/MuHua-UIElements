using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 饰品 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/饰品")]
public class ConstAccessory : ConstItem {
	/// <summary> 饰品类型 </summary>
	public AccessoryType type;

	public override DataItem To() {
		DataEquipment item = new DataEquipment();
		item.name = name;
		item.sprite = sprite;
		item.type = type.ToString();
		return item;
	}
}
/// <summary>
/// 饰品类型
/// </summary>
public enum AccessoryType {
	戒指, 手镯, 项链
}