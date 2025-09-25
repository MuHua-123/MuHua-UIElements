using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备
/// </summary>
public class Equipment : InventoryItem {
	/// <summary> 装备类型 （类型1/类型2） </summary>
	public string type;
	/// <summary> 词缀列表 </summary>
	public List<EquipmentAffix> affixes = new List<EquipmentAffix>();
}