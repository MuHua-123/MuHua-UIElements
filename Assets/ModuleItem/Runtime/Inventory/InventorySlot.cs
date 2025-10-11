using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 库存插槽
/// </summary>
public class InventorySlot {
	/// <summary> 数量 </summary>
	public int count;
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 物品 </summary>
	public InventoryItem item;
	/// <summary> 限制类型 </summary>
	public List<string> limits = new List<string>();

	/// <summary> 图片 </summary>
	public Sprite Sprite => item != null ? item.sprite : null;
	/// <summary> 堆叠数量 </summary>
	public int MaxStack => item != null ? item.stack : 0;

	/// <summary> 添加物品数量 </summary>
	public virtual void Settings(ref int count) {
		// 剩余容量
		int canAdd = MaxStack - count;
		// 剩余容量和余数取最小值
		int addCount = Mathf.Min(canAdd, count);
		this.count += addCount;
		count -= addCount;
	}
	/// <summary> 设置物品 </summary>
	public virtual void Settings(InventoryItem item, int count) {
		this.item = item;
		this.count = count;
	}
	/// <summary> 替换物品 </summary>
	public virtual (InventoryItem, int) Replace(InventoryItem item, int count) {
		int remain = this.count; this.count = count;
		InventoryItem old = this.item; this.item = item;
		return (old, remain);
	}

	/// <summary> 验证类型 </summary>
	public bool Verify(InventoryItem item) {
		string[] types = item.type.Split("/");
		return limits.All(item => types.Contains(item));
	}
	/// <summary> 是否可堆叠 </summary>
	public virtual bool IsStackable(InventoryItem obj) {
		return item != null && item.IsStackable(obj) && count < MaxStack;
	}
}
