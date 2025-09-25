using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 项目拖拽
/// </summary>
public interface InventoryDrag {
	/// <summary> 数量 </summary>
	public int Count { get; }
	/// <summary> 物品 </summary>
	public InventoryItem Item { get; }
	/// <summary> 锚点 </summary>
	public VisualElement Anchor { get; }

	/// <summary> 取消拖拽 </summary>
	public void Cancel();
	/// <summary> 设置物品 </summary>
	public void Settings(InventoryItem item, int count);
}
