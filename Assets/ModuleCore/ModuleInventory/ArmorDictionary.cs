using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备 - 字典
/// </summary>
public static class ArmorDictionary {
	/// <summary> 护甲 </summary>
	public static DataArmor Armor(string name, ArmorType armorType) {
		DataArmor armor = new DataArmor();
		armor.name = name;
		armor.itemType = ItemType.装备;
		armor.wearType = WearType.护甲;
		armor.armorType = armorType;
		return armor;
	}
	/// <summary> 法师袍 ac10 </summary>
	public static DataArmor Armor101() {
		DataArmor armor = Armor("法师袍", ArmorType.布甲);
		armor.additions.Add(new DataAddition { armorClass = 10 });
		return armor;
	}
	/// <summary> 皮甲 ac11 </summary>
	public static DataArmor Armor201() {
		DataArmor armor = Armor("皮甲", ArmorType.轻甲);
		armor.additions.Add(new DataAddition { armorClass = 11 });
		return armor;
	}
	/// <summary> 链甲 ac15 </summary>
	public static DataArmor Armor301() {
		DataArmor armor = Armor("链甲", ArmorType.中甲);
		armor.additions.Add(new DataAddition { armorClass = 15 });
		return armor;
	}
	/// <summary> 板甲 ac18 </summary>
	public static DataArmor Armor401() {
		DataArmor armor = Armor("板甲", ArmorType.重甲);
		armor.additions.Add(new DataAddition { armorClass = 18 });
		return armor;
	}
}
