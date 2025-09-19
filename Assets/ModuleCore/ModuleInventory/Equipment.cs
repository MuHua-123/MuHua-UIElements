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

	/// <summary> 添加插槽 </summary>
	public void AddSlot(EquipmentSlot slot) => dictionary.Add(slot.name, slot);
}
/// <summary>
/// 装备插槽
/// </summary>
public abstract class EquipmentSlot {
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 物品 </summary>
	public DataEquipment item;

	public EquipmentSlot(string name) => this.name = name;
}
/// <summary>
/// 武器 - 装备插槽
/// </summary>
public class WeaponSlot : EquipmentSlot {
	/// <summary> 副手插槽 </summary>
	public DeputySlot deputy;

	public WeaponSlot(string name) : base(name) { }
}
/// <summary>
/// 副手 - 装备插槽
/// </summary>
public class DeputySlot : EquipmentSlot {
	/// <summary> 主手插槽 </summary>
	public WeaponSlot weapon;

	public DeputySlot(string name) : base(name) { }
}
/// <summary>
/// 护甲 - 装备插槽
/// </summary>
public class ArmorSlot : EquipmentSlot {

	public ArmorSlot(string name) : base(name) { }

}
/// <summary>
/// 饰品 - 装备插槽
/// </summary>
public class AccessorySlot : EquipmentSlot {

	public AccessorySlot(string name) : base(name) { }

}