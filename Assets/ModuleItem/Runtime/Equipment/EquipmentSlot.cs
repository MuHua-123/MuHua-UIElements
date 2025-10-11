using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 装备插槽
/// </summary>
public class EquipmentSlot {
	/// <summary> 数量 </summary>
	public int count;
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 物品 </summary>
	public InventoryItem item;
	/// <summary> 限制类型 </summary>
	public List<string> limits = new List<string>();

	/// <summary> 堆叠数量 </summary>
	public int MaxStack => item != null ? item.stack : 0;
	/// <summary> 图片 </summary>
	public Sprite Sprite => item != null ? item.sprite : null;

	/// <summary> 验证类型 </summary>
	public bool Verify(InventoryItem item) {
		string[] types = item.type.Split("/");
		return limits.All(item => types.Contains(item));
	}

	/// <summary> 替换 </summary>
	public (InventoryItem, int) Replace(InventoryItem newItem, int count) {
		int remain = this.count;
		InventoryItem old = item;
		item = newItem;
		this.count = count;
		return (old, remain);
	}
}
