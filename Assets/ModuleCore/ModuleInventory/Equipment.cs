using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏
/// </summary>
public class Equipment {
	/// <summary> 插槽字典 </summary>
	public Dictionary<string, EquipmentSlot> dictionary = new Dictionary<string, EquipmentSlot>();

	/// <summary> 索引器 </summary>
	public EquipmentSlot this[string key] => dictionary[key];

	public bool ContainsKey(string key) => dictionary.ContainsKey(key);
	/// <summary> 添加插槽 </summary>
	public void AddSlot(EquipmentSlot slot) => dictionary.Add(slot.name, slot);
}
/// <summary>
/// 插槽类型
/// </summary>
public enum SlotType {
	库存, 主手, 副手, 上衣, 头盔, 手套, 腰带, 鞋子, 项链, 戒指1, 戒指2, 手镯1, 手镯2
}
/// <summary>
/// 装备插槽
/// </summary>
public abstract class EquipmentSlot {
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 物品 </summary>
	public DataEquipment item;

	public EquipmentSlot(SlotType slot) => name = slot.ToString();

	/// <summary> 设置 </summary>
	public void Settings(DataItem item, int count) {
		if (item is DataEquipment equipment) { this.item = equipment; }
		else { this.item = null; }
	}
	public abstract bool Verify(DataEquipment equipment);
}
/// <summary>
/// 武器 - 装备插槽
/// </summary>
public class WeaponSlot : EquipmentSlot {
	/// <summary> 副手插槽 </summary>
	public DeputySlot deputy;

	public WeaponSlot(SlotType slot) : base(slot) { }

	public override bool Verify(DataEquipment equipment) {
		string[] type = equipment.type.Split("/");
		if (type[0] == WeaponType.单手.ToString()) { return true; }
		if (type[0] == WeaponType.双手.ToString()) { return true; }
		return false;
	}
}
/// <summary>
/// 副手 - 装备插槽
/// </summary>
public class DeputySlot : EquipmentSlot {
	/// <summary> 主手插槽 </summary>
	public WeaponSlot weapon;

	public DeputySlot(SlotType slot) : base(slot) { }

	public override bool Verify(DataEquipment equipment) {
		string[] type = equipment.type.Split("/");
		if (type[0] == WeaponType.副手.ToString()) { return true; }
		return false;
	}
}
/// <summary>
/// 护甲 - 装备插槽
/// </summary>
public class ArmorSlot : EquipmentSlot {
	/// <summary> 护甲类型 </summary>
	public string armorType;

	public ArmorSlot(SlotType slot, ArmorType armor) : base(slot) => armorType = armor.ToString();

	public override bool Verify(DataEquipment equipment) {
		string[] type = equipment.type.Split("/");
		if (type[0] == armorType) { return true; }
		return false;
	}
}
/// <summary>
/// 饰品 - 装备插槽
/// </summary>
public class AccessorySlot : EquipmentSlot {
	/// <summary> 饰品类型 </summary>
	public string accessoryType;

	public AccessorySlot(SlotType slot, AccessoryType accessory) : base(slot) => accessoryType = accessory.ToString();

	public override bool Verify(DataEquipment equipment) {
		string[] type = equipment.type.Split("/");
		if (type[0] == accessoryType) { return true; }
		return false;
	}
}