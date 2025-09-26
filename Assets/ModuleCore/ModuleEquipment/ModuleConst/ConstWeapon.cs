using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器类型
/// </summary>
public enum WeaponType {
	单手, 副手, 双手
}
/// <summary>
/// 武器类别
/// </summary>
public enum WeaponCategory {
	剑, 刀, 枪, 弓, 法杖, 斧头, 锤子
}
/// <summary>
/// 武器 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/武器")]
public class ConstWeapon : ConstEquipment {
	/// <summary> 武器类型 </summary>
	public WeaponType type;
	/// <summary> 武器类别 </summary>
	public WeaponCategory category;

	public override InventoryItem To() {
		Equipment item = new Equipment();
		item.name = name;
		item.sprite = sprite;
		item.type = $"{type}/{category}";
		return item;
	}
}
