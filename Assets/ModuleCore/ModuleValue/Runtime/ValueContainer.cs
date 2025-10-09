using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数值容器
/// </summary>
public class ValueContainer {
	/// <summary> 数值字典 </summary>
	public Dictionary<string, ValueInstance> dictionary = new Dictionary<string, ValueInstance>();

	/// <summary> 所引器 </summary>
	public ValueInstance this[string id] => dictionary[id];

	/// <summary> 循环集合 </summary>
	public void ForEach(Action<string, ValueInstance> action) {
		foreach (var item in dictionary) { action?.Invoke(item.Key, item.Value); }
	}

	/// <summary> 添加数值 </summary>
	public void AddInstance(ValueType type, string name) {
		var instance = new ValueInstance(type, name);
		AddInstance(instance);
	}
	/// <summary> 添加数值 </summary>
	public void AddInstance(ValueInstance instance) {
		if (dictionary.ContainsKey(instance.name)) { return; }
		dictionary.Add(instance.name, instance);
	}
	/// <summary> 重新计算值 </summary>
	public void RecalculateValue() {
		foreach (var item in dictionary) { item.Value.RecalculateValue(); }
	}
}
