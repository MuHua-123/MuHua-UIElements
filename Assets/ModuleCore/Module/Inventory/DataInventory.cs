using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品库存 - 数据
/// </summary>
public abstract class DataInventory {
	/// <summary> 库存类型 </summary>
	public InventoryType inventoryType;

	/// <summary> 添加物品 </summary>
	public abstract bool Add(DataItem item);
	/// <summary> 移除物品 </summary>
	public abstract bool Remove(DataItem item);
}
/// <summary>
/// 库存类型枚举，区分背包、仓库等不同库存。
/// </summary>
public enum InventoryType { EquipmentSlot, Backpack, Storage }