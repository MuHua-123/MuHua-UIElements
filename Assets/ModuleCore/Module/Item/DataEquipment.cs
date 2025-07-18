using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备 - 数据
/// </summary>
public class DataEquipment : DataItem {
	/// <summary> 装备类型 </summary>
	public EquipmentType equipmentType;
}
/// <summary>
/// 武器 - 数据
/// </summary>
public class DataWeapon : DataEquipment {
	/// <summary> 武器类型 </summary>
	public WeaponType weaponType;
}
/// <summary>
/// 护甲 - 数据
/// </summary>
public class DataArmor : DataEquipment {
	/// <summary> 护甲类型 </summary>
	public ArmorType armorType;
}