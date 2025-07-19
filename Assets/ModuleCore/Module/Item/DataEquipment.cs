using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备类型。
/// </summary>
public enum EquipmentType {
	/// <summary> 武器 </summary>
	Weapon,
	/// <summary> 护甲 </summary>
	Armor,
	/// <summary> 头盔 </summary>
	Helmets,
	/// <summary> 手套 </summary>
	Gloves,
	/// <summary> 鞋子 </summary>
	Shoes,
}
/// <summary>
/// 装备 - 数据
/// </summary>
public class DataEquipment : DataItem {
	/// <summary> 装备类型 </summary>
	public EquipmentType equipmentType;
	/// <summary> 附加效果 </summary>
	public List<DataAddition> additions;

	/// <summary> 附加效果 </summary>
	public DataAddition Addition() {
		DataAddition addition = new DataAddition();
		additions.ForEach(obj => addition.Add(obj));
		return addition;
	}
}
