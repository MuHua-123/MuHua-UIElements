using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库存物品
/// </summary>
public class InventoryItem {
	/// <summary> 堆叠数量 </summary>
	public int stack = 1;
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 类型 </summary>
	public string type;
	/// <summary> 图片 </summary>
	public Sprite sprite;
	/// <summary> 预制件 </summary>
	public Transform prefabs;

	/// <summary> 是否可堆叠 </summary>
	public virtual bool IsStackable(InventoryItem item) {
		return name == item.name && type == item.type && stack > 1;
	}
}
