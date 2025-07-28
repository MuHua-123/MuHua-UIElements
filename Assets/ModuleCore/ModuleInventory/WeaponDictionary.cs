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

	/// <summary> 空手 1d2 </summary>
	public static DataWeapon Weapon000() {
		DataWeapon weapon = Weapon("空手", WeaponType.无);
		weapon.damageDice = new DataDamageDice(2, DamageType.无);
		return weapon;
	}

	/// <summary> 匕首 1d4 </summary>
	public static DataWeapon Weapon101() {
		DataWeapon weapon = Weapon("匕首", WeaponType.轻型武器);
		weapon.damageDice = new DataDamageDice(4, DamageType.穿刺);
		return weapon;
	}
	/// <summary> 短弓 1d6 </summary>
	public static DataWeapon Weapon102() {
		DataWeapon weapon = Weapon("短弓", WeaponType.轻型武器);
		weapon.damageDice = new DataDamageDice(6, DamageType.穿刺);
		return weapon;
	}

	/// <summary> 木棒 1d6 </summary>
	public static DataWeapon Weapon201() {
		DataWeapon weapon = Weapon("木棒", WeaponType.中型武器);
		weapon.damageDice = new DataDamageDice(6, DamageType.钝击);
		return weapon;
	}

	/// <summary> 巨棒 1d8 </summary>
	public static DataWeapon Weapon301() {
		DataWeapon weapon = Weapon("巨棒", WeaponType.重型武器);
		weapon.damageDice = new DataDamageDice(8, DamageType.钝击);
		return weapon;
	}
	/// <summary> 法杖 1d6 </summary>
	public static DataWeapon Weapon302() {
		DataWeapon weapon = Weapon("法杖", WeaponType.重型武器);
		weapon.damageDice = new DataDamageDice(6, DamageType.钝击);
		return weapon;
	}

	/// <summary> 木盾 ac1 </summary>
	public static DataWeapon Weapon401() {
		DataWeapon weapon = Weapon("木盾", WeaponType.盾牌);
		weapon.additions.Add(new DataAddition { armorClass = 1 });
		return weapon;
	}
	/// <summary> 箭袋 +20 </summary>
	public static DataWeapon Weapon402() {
		DataWeapon weapon = Weapon("箭袋", WeaponType.盾牌);
		weapon.additions.Add(new DataAddition { armorClass = 1 });
		return weapon;
	}
}
