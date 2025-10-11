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
		IEnumerable<int> enumerable = Enumerable.Range(0, capacity);
		slots = enumerable.Select(_ => new InventorySlot()).ToList();
	}

	/// <summary> 应用修改 </summary>
	public void Apply() => OnChange?.Invoke(this);
	/// <summary> 遍历数组 </summary>
	public void ForEach(Action<InventorySlot> action) => slots.ForEach(action);
	/// <summary> 获取一个空插槽 </summary>
	public InventorySlot Empty() => slots.FirstOrDefault(s => s.item == null);
	/// <summary> 相同堆叠插槽 </summary>
	public List<InventorySlot> Stackable(InventoryItem item) => slots.Where(slot => slot.IsStackable(item)).ToList();

	/// <summary> 添加物品 </summary>
	public void AddItem(InventoryItem item, int count) {
		// 合并重复可堆叠
		int remain = MergeToSlots(item, count);
		// 如果还有剩余，按照MaxStack分批放入空位
		if (remain > 0) { AddToSlots(item, remain, true); }
		OnChange?.Invoke(this);
	}

	/// <summary> 合并到插槽 </summary> 
	public int MergeToSlots(InventoryItem item, int count) {
		// 查询所有可合并的插槽
		var sameSlots = Stackable(item);
		// 合并到已有插槽
		sameSlots.ForEach(obj => obj.Settings(ref count));
		return count;
	}

	/// <summary> 添加物品 </summary>
	public void AddToSlots(InventoryItem item, int count, bool isWhile) {
		// 取一个空插槽
		InventorySlot emptySlot = Empty();
		// 没有空插槽则退出
		if (emptySlot == null) { return; }
		// 堆叠数量 = Min（物品数量 ,  最大堆叠数量）
		int addCount = Mathf.Min(count, item.stack);
		// 计算剩余物品数量
		int remain = count - addCount;
		// 填充进空插槽
		emptySlot.Settings(item, addCount);
		// 没有剩余或者不循环则退出
		if (remain == 0 || !isWhile) { return; }
		// 还有余量循环执行添加
		AddToSlots(item, remain, isWhile);
	}
}
