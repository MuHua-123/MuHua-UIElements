using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备词缀
/// </summary>
public class EquipmentAffix {
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 数值 </summary>
	public float value;
	/// <summary> 最大值 </summary>
	public float minValue;
	/// <summary> 最小值 </summary>
	public float maxValue = float.MaxValue;
	/// <summary> 属性类型 </summary>
	public AttributeType attributeType;
}
