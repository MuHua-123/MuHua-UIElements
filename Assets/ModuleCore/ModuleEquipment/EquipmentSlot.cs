using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 插槽类型
/// </summary>
public enum EquipmentSlotType {
	主手, 副手, 上衣, 项链,
	头盔, 手套, 腰带, 鞋子,
	戒指1, 戒指2, 手镯1, 手镯2
}
/// <summary>
/// 装备插槽
/// </summary>
public class EquipmentSlot {
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 物品 </summary>
	public Equipment item;
	/// <summary> 装备验证 </summary>
	public Func<Equipment, bool> verify;

	public EquipmentSlot(EquipmentSlotType slot, Func<Equipment, bool> verify) {
		name = slot.ToString();
		this.verify = verify;
	}

	/// <summary> 设置 </summary>
	public void Settings(InventoryItem item) {
		if (item is Equipment equipment) { this.item = equipment; }
		else { this.item = null; }
	}
}
