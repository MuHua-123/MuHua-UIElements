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
		type = ValueType.Float;
		minValue = 0;
		maxValue = 9999;
		baseValue = 0;
	}
	public void InitialInteger() {
		type = ValueType.Integer;
		minValue = 0;
		maxValue = 9999;
		baseValue = 0;
	}
	public void InitialBoolean() {
		type = ValueType.Boolean;
		minValue = 0;
		maxValue = 1;
		baseValue = 0;
	}
	public void InitialPercentage() {
		type = ValueType.Percentage;
		minValue = 0;
		maxValue = 100;
		baseValue = 0;
	}

	/// <summary> 转换数据 </summary>
	public ValueInstance To() {
		ValueInstance instance = new ValueInstance(type, name);
		instance.minValue = minValue;
		instance.maxValue = maxValue;
		instance.baseValue = baseValue;
		instance.RecalculateValue();
		return instance;
	}
}
