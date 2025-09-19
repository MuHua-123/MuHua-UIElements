using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/武器")]
public class ConstWeapon : ConstItem {
	/// <summary> 武器类型 </summary>
	public WeaponType type;

	public override DataItem To() {
		DataEquipment item = new DataEquipment();
		item.name = name;
		item.sprite = sprite;
		item.type = type.ToString();
		return item;
	}
}
/// <summary>
/// 武器类型
/// </summary>
public enum WeaponType {
	单手, 副手, 双手
}