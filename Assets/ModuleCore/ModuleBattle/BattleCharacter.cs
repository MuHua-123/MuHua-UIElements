using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗角色
/// </summary>
public class BattleCharacter : DataAttribute {
	/// <summary> 角色数据 </summary>
	public readonly DataCharacter character;

	/// <summary> 归属队伍 </summary>
	public int team;
	/// <summary> 角色名字 </summary>
	public string name;
	/// <summary> 战斗等级 </summary>
	public int level;
	/// <summary> 战场位置 </summary>
	public int position;
	/// <summary> 先攻顺序 </summary>
	public int sequence;
	/// <summary> 护甲等级 </summary>
	public int armorClass;
	/// <summary> 生命点 </summary>
	public Vector2Int hitPoint;

	public BattleCharacter(DataCharacter character, int team) {
		this.character = character;
		Cover(character);
		this.team = team;
		name = character.name;
		level = character.Level;
		position = 1;
		hitPoint = new Vector2Int(character.HitPoint, character.HitPoint);
		armorClass = character.ArmorClass;
	}
}
