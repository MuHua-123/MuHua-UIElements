using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数值实例
/// </summary>
public class ValueInstance {
	/// <summary> 数值类型 </summary>
	public readonly ValueType type;
	/// <summary> 数值名称 </summary>
	public readonly string name;

	/// <summary> 最小值 </summary>
	public float minValue;
	/// <summary> 最大值 </summary>
	public float maxValue;

	/// <summary> 基础值 </summary>
	public float baseValue;
	/// <summary> 附加值% </summary>
	public float addedValue;
	/// <summary> 当前值 </summary>
	public float currentValue;
	/// <summary> 最终数值 </summary>
	public float value;

	/// <summary> 修改器列表 </summary>
	public List<ValueModifier> modifiers = new List<ValueModifier>();

	/// <summary> 是布尔值 </summary>
	public bool isBoolean => type == ValueType.Boolean && value == maxValue;

	public ValueInstance(ValueType type, string name) {
		this.type = type;
		this.name = name;
		if (type == ValueType.Float) { minValue = 0; maxValue = float.MaxValue; }
		if (type == ValueType.Integer) { minValue = 0; maxValue = float.MaxValue; }
		if (type == ValueType.Boolean) { minValue = 0; maxValue = 1; }
		if (type == ValueType.Percentage) { minValue = 0; maxValue = 100; }
	}

	/// <summary> 添加修改器 </summary>
	public void AddModifier(ValueModifier modifier, bool isUpdate) {
		modifiers.Add(modifier);
		if (isUpdate) { RecalculateValue(); }
	}
	/// <summary> 添加修改器 </summary>
	public void AddModifier(List<ValueModifier> modifier, bool isUpdate) {
		modifiers.AddRange(modifier);
		if (isUpdate) { RecalculateValue(); }
	}
	/// <summary> 重新计算值 </summary>
	public void RecalculateValue() {
		addedValue = 1;
		currentValue = baseValue;
		modifiers.ForEach(Modify);
	}

	/// <summary> 修改 </summary>
	public void Modify(ValueModifier modifier) {
		addedValue += modifier.AddedValue;
		currentValue += modifier.FixedValue;
		value = currentValue * addedValue;
		value = Mathf.Clamp(currentValue, minValue, maxValue);
	}
}
