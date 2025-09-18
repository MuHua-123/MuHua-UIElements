using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品库存
/// </summary>
public class Inventory {
	/// <summary> 背包容量 </summary>
	public int capacity;
	/// <summary> 插槽列表 </summary>
	public List<InventorySlot> slots;
	/// <summary> 改变事件 </summary>
	public event Action<Inventory> OnChange;

	/// <summary> 物品库存 </summary>
	public Inventory(int capacity) {
		this.capacity = capacity;
		slots = new List<InventorySlot>();
		for (int i = 0; i < capacity; i++) { slots.Add(new InventorySlot()); }
	}

	/// <summary> 应用修改 </summary>
	public void Apply() => OnChange?.Invoke(this);
	/// <summary> 遍历数组 </summary>
	public void ForEach(Action<InventorySlot> action) => slots.ForEach(action);

	/// <summary> 添加物品 </summary>
	public void AddItem(DataItem item, int count) {
		int remain = MergeToSlots(item, count);
		// 如果还有剩余，按照MaxStack分批放入空位
		if (remain > 0) { AddToSlots(item, remain, true); }
		OnChange?.Invoke(this);
	}

	/// <summary> 添加物品 </summary>
	public void AddToSlots(DataItem item, int count, bool isWhile) {
		(bool isComplete, int remain) = AddToSlots(item, count);
		if (!isComplete || remain == 0 || !isWhile) { return; }
		AddToSlots(item, remain, isWhile);
	}
	/// <summary> 添加到插槽 bool = 是否添加成功，int = 余量 </summary>
	public (bool, int) AddToSlots(DataItem item, int count) {
		InventorySlot emptySlot = slots.FirstOrDefault(s => s.item == null);
		if (emptySlot == null) { return (false, count); }
		int addCount = Mathf.Min(count, item.MaxStack);
		emptySlot.Settings(item, addCount);
		return (true, count - addCount);
	}

	/// <summary> 合并到插槽 </summary> 
	public int MergeToSlots(DataItem item, int count) {
		// 查询所有可合并的插槽
		List<InventorySlot> sameSlots = slots.Where(slot => slot.Same(item)).ToList();
		// 合并到已有插槽
		sameSlots.ForEach(obj => MergeToSlots(obj, ref count));
		return count;
	}
	/// <summary> 合并到插槽 </summary>
	public void MergeToSlots(InventorySlot slot, ref int count) {
		int canAdd = slot.MaxStack - slot.count;
		int addCount = Mathf.Min(canAdd, count);
		slot.count += addCount;
		count -= addCount;
	}
}
/// <summary>
/// 库存插槽
/// </summary>
public class InventorySlot {
	/// <summary> 数量 </summary>
	public int count;
	/// <summary> 物品 </summary>
	public DataItem item;

	// /// <summary> 图片 </summary>
	public Sprite Sprite => item != null ? item.sprite : null;
	// /// <summary> 堆叠数量 </summary>
	public int MaxStack => item != null ? item.MaxStack : 0;

	public InventorySlot() { }

	public InventorySlot(DataItem item, int count) {
		this.item = item;
		this.count = count;
	}
	/// <summary> 设置 </summary>
	public void Settings(DataItem item, int count) {
		this.item = item;
		this.count = count;
	}
	/// <summary> 是否相同 </summary>
	public bool Same(DataItem obj) {
		return item != null && item.name == obj.name && count < MaxStack;
	}
}