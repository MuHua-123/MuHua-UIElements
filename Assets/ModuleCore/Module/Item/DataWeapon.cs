using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器类型。
/// </summary>
public enum WeaponType {
	/// <summary> 轻型武器 </summary>
	LightWeapon,
	/// <summary> 中型武器 </summary>
	MediumWeapon,
	/// <summary> 重型武器 </summary>
	HeavyWeapon,
	/// <summary> 盾牌 </summary>
	Shield,
}
/// <summary>
/// 武器 - 数据
/// </summary>
public class DataWeapon : DataEquipment {
	/// <summary> 武器类型 </summary>
	public WeaponType weaponType;
}
