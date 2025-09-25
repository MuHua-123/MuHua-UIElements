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
		AddSlot(new EquipmentSlot(EquipmentSlotType.主手, VerifyWeapon));
		AddSlot(new EquipmentSlot(EquipmentSlotType.副手, VerifyDeputy));

		AddSlot(new EquipmentSlot(EquipmentSlotType.上衣, (equipment) => VerifyCommon(equipment, ArmorType.上衣.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.头盔, (equipment) => VerifyCommon(equipment, ArmorType.头盔.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.手套, (equipment) => VerifyCommon(equipment, ArmorType.手套.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.腰带, (equipment) => VerifyCommon(equipment, ArmorType.腰带.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.鞋子, (equipment) => VerifyCommon(equipment, ArmorType.鞋子.ToString())));

		AddSlot(new EquipmentSlot(EquipmentSlotType.项链, (equipment) => VerifyCommon(equipment, AccessoryType.项链.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.戒指1, (equipment) => VerifyCommon(equipment, AccessoryType.戒指.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.戒指2, (equipment) => VerifyCommon(equipment, AccessoryType.戒指.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.手镯1, (equipment) => VerifyCommon(equipment, AccessoryType.手镯.ToString())));
		AddSlot(new EquipmentSlot(EquipmentSlotType.手镯2, (equipment) => VerifyCommon(equipment, AccessoryType.手镯.ToString())));
	}

	public bool ContainsKey(string key) => dictionary.ContainsKey(key);
	/// <summary> 添加插槽 </summary>
	public void AddSlot(EquipmentSlot slot) => dictionary.Add(slot.name, slot);

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