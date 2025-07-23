using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器 - 字典
/// </summary>
public static class WeaponDictionary {
	/// <summary> 武器 </summary>
	public static DataWeapon Weapon(string name, WeaponType weaponType) {
		DataWeapon weapon = new DataWeapon();
		weapon.name = name;
		weapon.itemType = ItemType.装备;
		weapon.wearType = WearType.武器;
		weapon.weaponType = weaponType;
		return weapon;
	}
	/// <summary> 伤害骰子 </summary>
	public static DataAddition DamageDice(int value, DamageType type) {
		DataDamageDice dice = new DataDamageDice(value, type);
		DataAddition addition = new DataAddition();
		addition.damageDices.Add(dice);
		return addition;
	}
	/// <summary> 匕首 1d4 </summary>
	public static DataWeapon Weapon101() {
		DataWeapon weapon = Weapon("匕首", WeaponType.轻型武器);
		weapon.additions.Add(DamageDice(4, DamageType.穿刺));
		return weapon;
	}
	/// <summary> 木棒 1d6 </summary>
	public static DataWeapon Weapon201() {
		DataWeapon weapon = Weapon("木棒", WeaponType.中型武器);
		weapon.additions.Add(DamageDice(6, DamageType.挥砍));
		return weapon;
	}
	/// <summary> 巨棒 1d8 </summary>
	public static DataWeapon Weapon301() {
		DataWeapon weapon = Weapon("巨棒", WeaponType.重型武器);
		weapon.additions.Add(DamageDice(8, DamageType.钝击));
		return weapon;
	}
	/// <summary> 木盾 ac1 </summary>
	public static DataWeapon Weapon401() {
		DataWeapon weapon = Weapon("木盾", WeaponType.盾牌);
		weapon.additions.Add(new DataAddition { armorClass = 1 });
		return weapon;
	}
}
