using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 材料 - 物品
/// </summary>
public class DataMaterial : DataItem {
	/// <summary> 最大堆叠数 </summary>
	public int maxStack;

	public override int MaxStack => maxStack;
}
