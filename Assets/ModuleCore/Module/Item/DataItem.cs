using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品类型。
/// </summary>
public enum ItemType {
	/// <summary> 材料 </summary>
	Material,
	/// <summary> 装备 </summary>
	Equipment
}
/// <summary>
/// 物品 - 数据
/// </summary>
public class DataItem {
	/// <summary> 物品名称 </summary>
	public string name;
	/// <summary> 物品类型 </summary>
	public ItemType itemType;
}

