using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏
/// </summary>
public class EquipmentColumn {
	/// <summary> 插槽字典 </summary>
	public Dictionary<string, EquipmentSlot> dictionary = new Dictionary<string, EquipmentSlot>();

	/// <summary> 索引器 </summary>
	public EquipmentSlot this[string key] => dictionary[key];

	public EquipmentColumn() {
		AddSlot(WeaponSlot());
		AddSlot(DeputySlot());

		AddSlot(ArmorSlot(EquipmentSlotType.上衣, ArmorType.上衣));
		AddSlot(ArmorSlot(EquipmentSlotType.头盔, ArmorType.头盔));
		AddSlot(ArmorSlot(EquipmentSlotType.手套, ArmorType.手套));
		AddSlot(ArmorSlot(EquipmentSlotType.腰带, ArmorType.腰带));
		AddSlot(ArmorSlot(EquipmentSlotType.鞋子, ArmorType.鞋子));

		AddSlot(AccessorySlot(EquipmentSlotType.项链, AccessoryType.项链));
		AddSlot(AccessorySlot(EquipmentSlotType.戒指1, AccessoryType.戒指));
		AddSlot(AccessorySlot(EquipmentSlotType.戒指2, AccessoryType.戒指));
		AddSlot(AccessorySlot(EquipmentSlotType.手镯1, AccessoryType.手镯));
		AddSlot(AccessorySlot(EquipmentSlotType.手镯2, AccessoryType.手镯));
	}

	public bool ContainsKey(string key) => dictionary.ContainsKey(key);
	/// <summary> 添加插槽 </summary>
	public void AddSlot(EquipmentSlot slot) => dictionary.Add(slot.name, slot);

	/// <summary> 武器插槽 </summary>
	public EquipmentSlot WeaponSlot() {
		return new EquipmentSlot(EquipmentSlotType.主手, VerifyWeapon);
	}
	/// <summary> 副手插槽 </summary>
	public EquipmentSlot DeputySlot() {
		return new EquipmentSlot(EquipmentSlotType.副手, VerifyDeputy);
	}
	/// <summary> 护甲插槽 </summary>
	public EquipmentSlot ArmorSlot(EquipmentSlotType type, ArmorType armor) {
		return new EquipmentSlot(type, (equipment) => VerifyCommon(equipment, armor.ToString()));
	}
	/// <summary> 饰品插槽 </summary>
	public EquipmentSlot AccessorySlot(EquipmentSlotType type, AccessoryType accessory) {
		return new EquipmentSlot(type, (equipment) => VerifyCommon(equipment, accessory.ToString()));
	}

	/// <summary> 通用校验 </summary>
	public static bool VerifyCommon(Equipment equipment, string type) {
		string[] types = equipment.type.Split("/");
		if (types[0] == type) { return true; }
		return false;
	}
	/// <summary> 主手校验 </summary>
	public static bool VerifyWeapon(Equipment equipment) {
		string[] type = equipment.type.Split("/");
		if (type[0] == WeaponType.单手.ToString()) { return true; }
		if (type[0] == WeaponType.双手.ToString()) { return true; }
		return false;
	}
	/// <summary> 副手校验 </summary>
	public static bool VerifyDeputy(Equipment equipment) {
		string[] type = equipment.type.Split("/");
		if (type[0] == WeaponType.副手.ToString()) { return true; }
		return false;
	}
}