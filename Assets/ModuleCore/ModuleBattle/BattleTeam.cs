using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗队伍
/// </summary>
public class BattleTeam {
	/// <summary> 队伍名字 </summary>
	public string name;
	/// <summary> 原始数据 </summary>
	public List<DataCharacter> characters = new List<DataCharacter>();
	/// <summary> 战斗数据 </summary>
	public List<BattleCharacter> battles = new List<BattleCharacter>();

	/// <summary> 添加角色 </summary>
	public void Add(List<DataCharacter> list) {
		characters.AddRange(list);
	}
	/// <summary> 添加角色 </summary>
	public void Add(DataCharacter obj) {
		characters.Add(obj);
	}
	/// <summary> 删除角色 </summary>
	public void Remove(DataCharacter character) {
		characters.Remove(character);
	}

	/// <summary> 战斗初始化 </summary>
	public void Initial(int id) {
		characters.ForEach(obj => battles.Add(new BattleCharacter(obj, id)));
	}
}