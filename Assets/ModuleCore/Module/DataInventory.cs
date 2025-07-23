using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库存类型枚举，区分背包、仓库等不同库存。
/// </summary>
public enum InventoryType { EquipmentSlot, Backpack, Storage }
/// <summary>
/// 物品库存 - 数据
/// </summary>
public abstract class DataInventory {
	/// <summary> 库存类型 </summary>
	public InventoryType inventoryType;
}
/// <summary>
/// 仓库 - 库存
/// </summary>
public class DataStorage : DataInventory {

}
/// <summary>
/// 背包 - 库存
/// </summary>
public class DataBackpack : DataInventory {

}