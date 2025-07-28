using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏 - 库存
/// </summary>
public class DataEquipment {
	/// <summary> 武器1 </summary>
	public DataWeapon weapon1;
	/// <summary> 武器2 </summary>
	public DataWeapon weapon2;
	/// <summary> 护甲 </summary>
	public DataArmor armor;
	/// <summary> 头盔 </summary>
	public DataWear helmets;
	/// <summary> 手套 </summary>
	public DataWear gloves;
	/// <summary> 鞋子 </summary>
	public DataWear shoes;

	/// <summary> 附加效果 </summary>
	public DataAddition addition = new DataAddition();
}
/// <summary>
/// 附加 - 数据
/// </summary>
public class DataAddition {
	/// <summary> 护甲等级 </summary>
	public int armorClass = 0;
}
