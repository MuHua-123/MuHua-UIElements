using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

/// <summary>
/// 物品类型
/// </summary>
public enum ItemType { 材料, 装备 }
/// <summary>
/// 物品 - 数据
/// </summary>
public class DataItem {
	/// <summary> 物品名称 </summary>
	public string name;
	/// <summary> 物品类型 </summary>
	public ItemType itemType;
}
/// <summary>
/// 材料 - 数据
/// </summary>
public class DataMaterial : DataItem {
	/// <summary> 物品数量 </summary>
	public int quantity;
	/// <summary> 堆叠上限 </summary>
	public int maxStack;
}
/// <summary>
/// 穿戴类型
/// </summary>
public enum WearType { 武器, 护甲, 头盔, 手套, 鞋子, }
/// <summary>
/// 穿戴 - 数据
/// </summary>
public class DataWear : DataItem {
	/// <summary> 装备类型 </summary>
	public WearType wearType;
	/// <summary> 附加列表 </summary>
	public List<DataAddition> additions = new List<DataAddition>();
	/// <summary> 附加效果 </summary>
	public virtual DataAddition Addition => EquipmentTool.Merge(additions);
}
/// <summary>
/// 武器类型。
/// </summary>
public enum WeaponType { 无, 轻型武器, 中型武器, 重型武器, 盾牌, }
/// <summary>
/// 武器 - 数据
/// </summary>
public class DataWeapon : DataWear {
	/// <summary> 武器类型 </summary>
	public WeaponType weaponType;
	/// <summary> 伤害骰子 </summary>
	public DataDamageDice damageDice;
}
/// <summary>
/// 伤害类型
/// </summary>
public enum DamageType { 无, 穿刺, 挥砍, 钝击 }
/// <summary>
/// 伤害骰子 - 数据
/// </summary>
public class DataDamageDice {
	/// <summary> 伤害骰子 </summary>
	public readonly int value;
	/// <summary> 伤害类型 </summary>
	public readonly DamageType type;

	public DataDamageDice(int value, DamageType type) {
		this.value = value;
		this.type = type;
	}
}
/// <summary>
/// 护甲类型。
/// </summary>
public enum ArmorType { 布甲, 轻甲, 中甲, 重甲, }
/// <summary>
/// 护甲 - 数据
/// </summary>
public class DataArmor : DataWear {
	/// <summary> 护甲类型 </summary>
	public ArmorType armorType;
}