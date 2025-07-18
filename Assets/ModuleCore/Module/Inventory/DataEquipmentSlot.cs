using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏 - 数据
/// </summary>
public class DataEquipmentSlot : DataInventory {
	/// <summary> 武器1 </summary>
	public DataWeapon Weapon1;
	/// <summary> 武器2 </summary>
	public DataWeapon Weapon2;
	/// <summary> 护甲 </summary>
	public DataArmor Armor;
	/// <summary> 头盔 </summary>
	public DataEquipment Helmets;
	/// <summary> 手套 </summary>
	public DataEquipment Gloves;
	/// <summary> 鞋子 </summary>
	public DataEquipment Shoes;

	public override bool Add(DataItem item) {
		// 如果是武器，则尝试装备
		if (item is DataWeapon weapon) { return Wear(weapon); }
		// 如果是护甲，则尝试装备
		if (item is DataArmor armor) { return Wear(armor); }
		// 如果是其他，则尝试装备
		if (item is DataEquipment equipment) { return Wear(equipment); }
		return false;
	}
	public override bool Remove(DataItem item) {
		throw new System.NotImplementedException();
	}

	private bool Wear(DataWeapon weapon) {
		Weapon1 = weapon;
		return true;
	}
	private bool Wear(DataArmor armor) {
		Armor = armor;
		return true;
	}
	private bool Wear(DataEquipment equipment) {
		if (equipment.equipmentType == EquipmentType.Helmets) { Helmets = equipment; return true; }
		if (equipment.equipmentType == EquipmentType.Gloves) { Gloves = equipment; return true; }
		if (equipment.equipmentType == EquipmentType.Shoes) { Shoes = equipment; return true; }
		return false;
	}
}
