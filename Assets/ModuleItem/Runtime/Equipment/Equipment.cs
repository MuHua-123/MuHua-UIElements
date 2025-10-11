using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏
/// 装备类型：稀有度/装备类型/次级类型/种类
/// <br/>稀有度：普通，精良，稀有，史诗，传奇，神话
/// <br/>装备类型：武器，护甲，饰品
/// <br/>次级类型：单手，副手，双手，上衣，头盔，手套，腰带，鞋子，项链，戒指，手镯
/// <br/>装备种类：剑，锤，枪，弓，斧头，盾牌，法杖，魔杖
/// </summary>
public class Equipment {
	/// <summary> 插槽字典 </summary>
	public Dictionary<string, InventorySlot> dictionary = new Dictionary<string, InventorySlot>();

	/// <summary> 索引器 </summary>
	public InventorySlot this[string key] => dictionary[key];
	/// <summary> 索引器(枚举) </summary>
	public InventorySlot this[Enum key] => dictionary[key.ToString()];

	/// <summary> 遍历数组 </summary>
	public void ForEach(Action<string, InventorySlot> action) {
		foreach (var item in dictionary) { action?.Invoke(item.Key, item.Value); }
	}

	/// <summary> 添加插槽 </summary>
	public void AddSlot(Enum name, string type) {
		AddSlot(name.ToString(), type);
	}
	/// <summary> 添加插槽 </summary>
	public void AddSlot(string name, string type) {
		var slot = new InventorySlot();
		slot.name = name;
		slot.limits = new List<string>(type.Split("/"));
		AddSlot(slot);
	}
	/// <summary> 添加插槽 </summary>
	public void AddSlot(InventorySlot slot) {
		if (dictionary.ContainsKey(slot.name)) { return; }
		dictionary.Add(slot.name, slot);
	}
}