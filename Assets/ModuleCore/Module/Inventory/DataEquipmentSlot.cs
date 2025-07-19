using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏 - 数据
/// </summary>
public class DataEquipmentSlot : DataInventory {
	/// <summary> 武器1 </summary>
	public DataWeapon weapon1;
	/// <summary> 武器2 </summary>
	public DataWeapon weapon2;
	/// <summary> 护甲 </summary>
	public DataArmor armor;
	/// <summary> 头盔 </summary>
	public DataEquipment helmets;
	/// <summary> 手套 </summary>
	public DataEquipment gloves;
	/// <summary> 鞋子 </summary>
	public DataEquipment shoes;

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

	/// <summary> 获取护甲等级 </summary>
	public int GetArmorClass(int modifier) {
		int addValue = GetAddValue();
		// 无甲 基础AC = 10，调整值全额生效
		if (armor == null) { return 10 + modifier + addValue; }
		// 布甲 调整值全额生效
		if (armor.armorType == ArmorType.ClothArmor) { }
		// 轻甲 调整值全额生效
		if (armor.armorType == ArmorType.LightArmor) { }
		// 中甲 调整值上限=2
		if (armor.armorType == ArmorType.MediumArmor) { modifier = Mathf.Min(modifier, 2); }
		// 重甲 调整值无效‌
		if (armor.armorType == ArmorType.HeavyArmor) { modifier = 0; }
		// 基础AC + 调整值 + 附加值
		return GetArmorClass(armor) + modifier + addValue;
	}
	/// <summary> 获取护甲等级 </summary>
	public int GetArmorClass(DataEquipment equipment) {
		return equipment.Addition().armorClass;
	}
	/// <summary> 获取护甲加值 </summary>
	public int GetAddValue() {
		int addValue = 0;
		// 武器1 加值
		if (weapon1 != null) { addValue += GetArmorClass(weapon1); }
		// 武器2 加值
		if (weapon2 != null) { addValue += GetArmorClass(weapon2); }
		// 头盔 加值
		if (helmets != null) { addValue += GetArmorClass(helmets); }
		// 手套 加值
		if (gloves != null) { addValue += GetArmorClass(gloves); }
		// 鞋子 加值
		if (shoes != null) { addValue += GetArmorClass(shoes); }
		return addValue;
	}

	#region 穿戴
	private bool Wear(DataWeapon weapon) {
		weapon1 = weapon;
		return true;
	}
	private bool Wear(DataArmor armor) {
		this.armor = armor;
		return true;
	}
	private bool Wear(DataEquipment equipment) {
		if (equipment.equipmentType == EquipmentType.Helmets) { helmets = equipment; return true; }
		if (equipment.equipmentType == EquipmentType.Gloves) { gloves = equipment; return true; }
		if (equipment.equipmentType == EquipmentType.Shoes) { shoes = equipment; return true; }
		return false;
	}
	#endregion
}
