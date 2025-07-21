using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗角色
/// </summary>
public class BattleCharacter : DataAttribute {
	/// <summary> 角色数据 </summary>
	public readonly DataCharacter character;

	/// <summary> 角色名字 </summary>
	public string name;
	/// <summary> 战斗等级 </summary>
	public int level;
	/// <summary> 生命点 </summary>
	public int hitPoint;
	/// <summary> 护甲等级 </summary>
	public int armorClass;
	/// <summary> 先攻顺序 </summary>
	public int sequence;

	public BattleCharacter(DataCharacter character) {
		this.character = character;
		Cover(character);
		name = character.name;
		level = character.Level;
		hitPoint = character.HitPoint;
		armorClass = character.ArmorClass;
	}

}
