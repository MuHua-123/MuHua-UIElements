using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库存插槽
/// </summary>
public class InventorySlot {
	/// <summary> 数量 </summary>
	public int count;
	/// <summary> 物品 </summary>
	public InventoryItem item;

	// /// <summary> 图片 </summary>
	public Sprite Sprite => item != null ? item.sprite : null;
	// /// <summary> 堆叠数量 </summary>
	public int MaxStack => item != null ? item.MaxStack : 0;

	public InventorySlot() { }

	public InventorySlot(InventoryItem item, int count) {
		this.item = item;
		this.count = count;
	}
	/// <summary> 设置 </summary>
	public void Settings(InventoryItem item, int count) {
		this.item = item;
		this.count = count;
	}
	/// <summary> 是否相同 </summary>
	public bool Same(InventoryItem obj) {
		return item != null && item.name == obj.name && count < MaxStack;
	}
}
