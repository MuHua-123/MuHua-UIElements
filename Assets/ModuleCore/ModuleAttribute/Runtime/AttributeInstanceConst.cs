using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性实例 - 常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/属性模块/属性实例")]
public class AttributeInstanceConst : ScriptableObject {
	/// <summary> 属性类型 </summary>
	public AttributeType type;
	/// <summary> 默认值 </summary>
	public float defaultValue;
	/// <summary> 最小值 </summary>
	public float minValue;
	/// <summary> 最大值 </summary>
	public float maxValue = 9999;

	/// <summary> 属性容器 </summary>
	[HideInInspector]
	public AttributeContainerConst container;

	public void InitialFloat() {
		type = AttributeType.Float;
		defaultValue = 0;
		minValue = 0;
		maxValue = 9999;
	}
	public void InitialInteger() {
		type = AttributeType.Integer;
		defaultValue = 0;
		minValue = 0;
		maxValue = 9999;
	}
	public void InitialBoolean() {
		type = AttributeType.Boolean;
		defaultValue = 0;
		minValue = 0;
		maxValue = 1;
	}
	public void InitialPercentage() {
		type = AttributeType.Percentage;
		defaultValue = 0;
		minValue = 0;
		maxValue = 100;
	}

	/// <summary> 转换数据 </summary>
	public AttributeInstance To() {
		AttributeInstance instance = new AttributeInstance();
		instance.type = type;
		instance.name = name;
		instance.baseValue = defaultValue;
		instance.currentValue = defaultValue;
		instance.minValue = minValue;
		instance.maxValue = maxValue;
		return instance;
	}
}
