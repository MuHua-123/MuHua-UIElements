using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 物品 - 管理器
/// </summary>
public class ManagerItem : ModuleSingle<ManagerItem> {
	/// <summary> 装备列表 </summary>
	public List<EquipmentConst> equipments;

	protected override void Awake() => NoReplace(false);

}
/// <summary>
/// 物品奖励
/// </summary>
public class ItemReward {
	/// <summary> 物品列表 </summary>
	public List<InventoryItem> items;

	public void Settings<T>(List<T> list) where T : InventoryItemConst {
		items = new List<InventoryItem>();
		list.ForEach(obj => items.Add(obj.To()));
	}

	public (InventoryItem, int) Get(int max = 1) {
		int count = Random.Range(1, max + 1);
		int index = Random.Range(0, items.Count);
		return (items[index], count);
	}
}