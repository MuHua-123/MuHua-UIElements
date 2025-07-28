using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗角色 - 工具
/// </summary>
public static class CombatRoleTool {

	#region 设置参数
	/// <summary> 初始角色 </summary>
	public static void Initial(this DataCombatRole role) {
		AttributeTool.Cover(role, role.character);
		role.name = role.character.name;
		role.level = role.character.Level;
		role.hitPoint = new Vector2Int(role.character.HitPoint, role.character.HitPoint);
		role.armorClass = role.character.ArmorClass;
		role.weapon1 = role.character.equipment.weapon1;
		role.weapon2 = role.character.equipment.weapon2;
		if (role.weapon1 == null) { role.weapon1 = WeaponDictionary.Weapon000(); }
		if (role.weapon2 == null) { role.weapon2 = WeaponDictionary.Weapon000(); }
	}
	/// <summary> 设置队伍 </summary>
	public static void Settings(this DataCombatRole role, int team, int position) {
		role.team = team;
		role.position = position;
	}
	#endregion

}
