using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 护甲类型。
/// </summary>
public enum ArmorType {
	/// <summary> 布甲 </summary>
	ClothArmor,
	/// <summary> 轻甲 </summary>
	LightArmor,
	/// <summary> 中甲 </summary>
	MediumArmor,
	/// <summary> 重甲 </summary>
	HeavyArmor,
}
/// <summary>
/// 护甲 - 数据
/// </summary>
public class DataArmor : DataEquipment {
	/// <summary> 护甲类型 </summary>
	public ArmorType armorType;
}
