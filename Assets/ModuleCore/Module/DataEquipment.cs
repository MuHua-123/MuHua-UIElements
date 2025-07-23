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
	/// <summary> 伤害骰子 </summary>
	public List<DataDamageDice> damageDices = new List<DataDamageDice>();
}
/// <summary>
/// 伤害类型
/// </summary>
public enum DamageType { 穿刺, 挥砍, 钝击 }
/// <summary>
/// 伤害 - 数据
/// </summary>
public class DataDamageDice {
	/// <summary> 伤害骰子 </summary>
	public readonly int dice;
	/// <summary> 伤害类型 </summary>
	public readonly DamageType damageType;

	public DataDamageDice(int dice, DamageType damageType) {
		this.dice = dice;
		this.damageType = damageType;
	}
}