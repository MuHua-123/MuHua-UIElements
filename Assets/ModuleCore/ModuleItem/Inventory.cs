using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品库存
/// </summary>
public class Inventory {
	/// <summary> 插槽列表 </summary>
	public List<InventorySlot> slots = new List<InventorySlot>();

	/// <summary> 添加物品 </summary>
	public void AddItem(InventorySlot slot) {
		if (MergeToSlots(slot)) { return; }
		// 如果还有剩余，按照MaxStack分批放入空位
		while (slot.count > 0) { slot.count = AddToSlots(slot); }
	}
	/// <summary> 添加到插槽 </summary>
	public int AddToSlots(InventorySlot slot) {
		InventorySlot emptySlot = slots.FirstOrDefault(s => s.item == null);
		if (emptySlot == null) { Debug.Log("库存没有空位了!"); return 0; }
		// 填充插槽
		int addCount = Mathf.Min(slot.count, slot.MaxStack);
		emptySlot.Settings(slot.item, addCount);
		// 计算余量
		return slot.count - addCount;
	}
	/// <summary> 合并到插槽 </summary>
	public bool MergeToSlots(InventorySlot slot) {
		int remain = slot.count;
		// 查询所有可合并的插槽
		var sameSlots = slots.Where(slot => slot.Merge(slot)).ToList();
		// 合并到已有插槽
		sameSlots.ForEach(obj => MergeToSlots(obj, ref remain));
		slot.count = remain;
		return remain == 0;
	}
	/// <summary> 合并到插槽 </summary>
	public void MergeToSlots(InventorySlot slot, ref int remain) {
		int canAdd = slot.MaxStack - slot.count;
		int add = Mathf.Min(canAdd, remain);
		slot.count += add;
		remain -= add;
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

	/// <summary> 堆叠数量 </summary>
	public virtual int MaxStack => item != null ? item.MaxStack : 0;

	public InventorySlot() { }

	public InventorySlot(DataItem item, int count) {
		this.item = item;
		this.count = count;
	}
	public void Settings(DataItem item, int count) {
		this.item = item;
		this.count = count;
	}

	/// <summary> 是否允许合并 </summary>
	public bool Merge(InventorySlot slot) {
		if (item == null || slot == null || slot.item == null) { return false; }
		if (count >= item.MaxStack) { return false; }
		return slot.item.name == item.name;
	}
}