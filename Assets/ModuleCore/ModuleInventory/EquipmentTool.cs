using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏 - 工具
/// </summary>
public static class EquipmentTool {

	#region 附加
	/// <summary> 添加属性 </summary>
	public static void Add(this DataAddition a, DataAddition b) {
		a.armorClass += b.armorClass;
	}
	/// <summary> 合并属性 </summary>
	public static DataAddition Merge(List<DataAddition> additions) {
		DataAddition addition = new DataAddition();
		additions.ForEach(obj => addition.Add(obj));
		return addition;
	}
	/// <summary> 更新附加值 </summary>
	public static void UpdateAddition(this DataEquipment equipment) {
		DataAddition addition = new DataAddition();
		if (equipment.weapon1 != null) { addition.Add(equipment.weapon1.Addition); }
		if (equipment.weapon2 != null) { addition.Add(equipment.weapon2.Addition); }
		if (equipment.armor != null) { addition.Add(equipment.armor.Addition); }
		if (equipment.helmets != null) { addition.Add(equipment.helmets.Addition); }
		if (equipment.gloves != null) { addition.Add(equipment.gloves.Addition); }
		if (equipment.shoes != null) { addition.Add(equipment.shoes.Addition); }
		equipment.addition = addition;
	}
	/// <summary> 获取护甲等级 </summary>
	public static int ArmorClass(this DataEquipment equipment, int modifier) {
		int addValue = equipment.addition.armorClass;
		// 无甲 基础AC = 10 + 调整值 + 附加值
		if (equipment.armor == null) { return 10 + modifier + addValue; }
		// 布甲/轻甲 调整值全额生效
		if (equipment.armor.armorType == ArmorType.布甲) { }
		//  调整值全额生效
		if (equipment.armor.armorType == ArmorType.轻甲) { }
		// 中甲 调整值上限=2
		if (equipment.armor.armorType == ArmorType.中甲) { modifier = Mathf.Min(modifier, 2); }
		// 重甲 调整值无效‌
		if (equipment.armor.armorType == ArmorType.重甲) { modifier = 0; }
		// 调整值 + 附加值
		return modifier + addValue;
	}
	#endregion

	#region 穿戴
	/// <summary> 穿戴装备 </summary>
	public static bool Wear(this DataEquipment equipment, DataWear wear, out DataWear old) {
		old = wear; bool isWear = false;
		if (wear.wearType == WearType.武器) { isWear = WearWeapon(equipment, wear, out old); }
		if (wear.wearType == WearType.护甲) { isWear = WearArmor(equipment, wear, out old); }
		if (wear.wearType == WearType.头盔) { isWear = WearHelmets(equipment, wear, out old); }
		if (wear.wearType == WearType.手套) { isWear = WearGloves(equipment, wear, out old); }
		if (wear.wearType == WearType.鞋子) { isWear = WearShoes(equipment, wear, out old); }
		equipment.UpdateAddition();
		return isWear;
	}
	/// <summary> 穿戴武器 </summary>
	public static bool WearWeapon(this DataEquipment equipment, DataWear wear, out DataWear old) {
		old = null;
		// 校验是否为武器类型
		if (!(wear is DataWeapon newWeapon)) { return false; }
		// 情况1：主手为空，直接装备主手
		if (equipment.weapon1 == null) { equipment.weapon1 = newWeapon; return true; }
		// 情况2：副手为空，直接装备副手
		if (equipment.weapon2 == null) { equipment.weapon2 = newWeapon; return true; }
		// 情况3：主手副手都不为空，默认替换主手
		old = equipment.weapon1;
		equipment.weapon1 = newWeapon;
		equipment.UpdateAddition();
		return true;
	}
	/// <summary> 穿戴护甲 </summary>
	public static bool WearArmor(this DataEquipment equipment, DataWear wear, out DataWear old) {
		old = null;
		// 校验是否为护甲类型
		if (!(wear is DataArmor newArmor)) { return false; }
		old = equipment.armor;
		equipment.armor = newArmor;
		return true;
	}
	/// <summary> 穿戴头盔 </summary>
	public static bool WearHelmets(this DataEquipment equipment, DataWear wear, out DataWear old) {
		old = equipment.helmets;
		equipment.helmets = wear;
		return true;
	}
	/// <summary> 穿戴手套 </summary>
	public static bool WearGloves(this DataEquipment equipment, DataWear wear, out DataWear old) {
		old = equipment.gloves;
		equipment.gloves = wear;
		return true;
	}
	/// <summary> 穿戴鞋子 </summary>
	public static bool WearShoes(this DataEquipment equipment, DataWear wear, out DataWear old) {
		old = equipment.shoes;
		equipment.shoes = wear;
		return true;
	}
	#endregion
}
