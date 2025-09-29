using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/武器")]
public class WeaponConst : EquipmentConst {
	/// <summary> 武器类型 </summary>
	public WeaponType type;
	/// <summary> 武器类别 </summary>
	public string category;

	public override InventoryItem To() {
		Equipment item = new Equipment();
		item.name = name;
		item.sprite = sprite;
		item.type = $"{type}/{category}";
		return item;
	}
}
