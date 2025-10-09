using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数值容器 - 常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/数值模块/数值容器")]
public class ValueContainerConst : ScriptableObject {
	/// <summary> 属性列表 </summary>
	[HideInInspector]
	public List<ValueInstanceConst> instances = new List<ValueInstanceConst>();

	/// <summary> 转换数据 </summary>
	public ValueContainer To() {
		ValueContainer container = new ValueContainer();
		return To(container);
	}
	/// <summary> 转换数据 </summary>
	public ValueContainer To(ValueContainer container) {
		instances.ForEach(obj => container.AddInstance(obj.To()));
		return container;
	}
}
