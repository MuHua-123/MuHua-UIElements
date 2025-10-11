using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数值实例 - 常量
/// </summary>
// [CreateAssetMenu(menuName = "MuHua/数值模块/数值实例")]
public class ValueInstanceConst : ScriptableObject {
	/// <summary> 数值类型 </summary>
	public ValueType type;

	/// <summary> 最小值 </summary>
	public float minValue;
	/// <summary> 最大值 </summary>
	public float maxValue = 9999;

	/// <summary> 基础值 </summary>
	public float baseValue;

	/// <summary> 数值容器 </summary>
	[HideInInspector]
	public ValueContainerConst container;

	public void InitialFloat() {
		Initial(ValueType.Float, 0, 9999, 0);
	}
	public void InitialInteger() {
		Initial(ValueType.Integer, 0, 9999, 0);
	}
	public void InitialBoolean() {
		Initial(ValueType.Boolean, 0, 1, 0);
	}
	public void InitialPercentage() {
		Initial(ValueType.Percentage, 0, 100, 0);
	}
	public void Initial(ValueType type, float minValue, float maxValue, float baseValue) {
		this.type = type;
		this.minValue = minValue;
		this.maxValue = maxValue;
		this.baseValue = baseValue;
	}

	/// <summary> 转换数据 </summary>
	public ValueInstance To() {
		var instance = ValueInstance.Create(type, name);
		instance.Settings(baseValue, minValue, maxValue);
		return instance;
	}
}
