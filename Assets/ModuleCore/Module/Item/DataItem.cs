using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品 - 数据
/// </summary>
public class DataItem {
	/// <summary> 物品名称 </summary>
	public string name;
	/// <summary> 物品类型 </summary>
	public ItemType itemType;
}
/// <summary>
/// 物品类型。
/// </summary>
public enum ItemType {
	Material, // 材料
	Equipment  // 装备
}
/// <summary>
/// 装备类型。
/// </summary>
public enum EquipmentType {
	Weapon, // 武器
	Armor, // 护甲
	Helmets, //头盔
	Gloves, // 手套
	Shoes, // 鞋子
}
/// <summary>
/// 武器类型。
/// </summary>
public enum WeaponType {
	LightWeapon, // 轻型武器
	MediumWeapon, // 中型武器
	HeavyWeapon, //重型武器
	Shield, // 盾牌
}
/// <summary>
/// 护甲类型。
/// </summary>
public enum ArmorType {
	ClothArmor, // 布甲
	LightArmor, // 轻甲
	MediumArmor, // 中甲
	HeavyArmor, // 重甲
}