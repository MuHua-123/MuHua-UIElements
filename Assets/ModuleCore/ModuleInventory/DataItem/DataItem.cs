using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品
/// </summary>
public class DataItem {
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 图片 </summary>
	public Sprite sprite;

	/// <summary> 堆叠数量 </summary>
	public virtual int MaxStack => 1;
}
