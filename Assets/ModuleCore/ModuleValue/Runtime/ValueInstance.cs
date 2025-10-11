using System;
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

	/// <summary> 自定义数值 </summary>
	public float value;

	/// <summary> 修改器列表 </summary>
	public List<ValueModifier> modifiers = new List<ValueModifier>();

	/// <summary> 默认值 </summary>
	public float DefaultValue { get; protected set; }
	/// <summary> 最小值 </summary>
	public float MinValue { get; protected set; }
	/// <summary> 最大值 </summary>
	public float MaxValue { get; protected set; }
	/// <summary> 基础值 = 默认值 + 全部修改器 </summary>
	public float BaseValue { get; protected set; }
	/// <summary> 附加值% = 1 + 全部修改器 </summary>
	public float AddedValue { get; protected set; }
	/// <summary> 当前值 = Clamp(基础值 * 附加值%) </summary>
	public float CurrentValue { get; protected set; }
	/// <summary> 是布尔值 </summary>
	public bool IsBoolean => type == ValueType.Boolean && CurrentValue == MaxValue;

	/// <summary> 创建数值(用枚举命名) </summary>
	public static ValueInstance Create(ValueType type, Enum name) {
		return Create(type, name.ToString());
	}
	/// <summary> 创建数值 </summary>
	public static ValueInstance Create(ValueType type, string name) {
		ValueInstance instance = new ValueInstance(type, name);
		float maxValue = float.MaxValue;
		if (type == ValueType.Boolean) { maxValue = 1; }
		if (type == ValueType.Percentage) { maxValue = 100; }
		instance.Settings(0, maxValue);
		return instance;
	}

	protected ValueInstance(ValueType type, string name) {
		this.type = type;
		this.name = name;
	}

	/// <summary> 设置默认值 </summary>
	public void Settings(float defaultValue) {
		this.DefaultValue = defaultValue;
		RecalculateValue();
	}
	/// <summary> 设置数值范围 </summary>
	public void Settings(float minValue, float maxValue) {
		this.MinValue = minValue;
		this.MaxValue = maxValue;
		RecalculateValue();
	}
	/// <summary> 设置默认值和数值范围 </summary>
	public void Settings(float defaultValue, float minValue, float maxValue) {
		this.DefaultValue = defaultValue;
		this.MinValue = minValue;
		this.MaxValue = maxValue;
		RecalculateValue();
	}
	/// <summary> 添加修改器 </summary>
	public void AddModifier(ValueModifier modifier, bool isUpdate = true) {
		modifiers.Add(modifier);
		if (isUpdate) { RecalculateValue(); }
	}
	/// <summary> 添加修改器 </summary>
	public void AddModifier(List<ValueModifier> modifier, bool isUpdate = true) {
		modifiers.AddRange(modifier);
		if (isUpdate) { RecalculateValue(); }
	}
	/// <summary> 重新计算值 </summary>
	public void RecalculateValue() {
		AddedValue = 1;
		BaseValue = DefaultValue;
		modifiers.ForEach(Modify);
		float temp = BaseValue * AddedValue;
		CurrentValue = Mathf.Clamp(temp, MinValue, MaxValue);
		if (type == ValueType.Integer) { CurrentValue = Mathf.Round(CurrentValue); }
	}

	/// <summary> 修改 </summary>
	private void Modify(ValueModifier modifier) {
		BaseValue += modifier.FixedValue;
		AddedValue += modifier.AddedValue;
	}
}
