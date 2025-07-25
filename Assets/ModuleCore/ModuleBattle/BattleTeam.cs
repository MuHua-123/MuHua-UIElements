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
	public List<DataCombatRole> battles = new List<DataCombatRole>();

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

	/// <summary> 初始化 </summary>
	public void Initial() {
		characters.ForEach(Initial);
	}
	/// <summary> 初始化 </summary>
	public void Initial(DataCharacter character) {
		DataCombatRole role = new DataCombatRole(character);
		role.Initial();
		battles.Add(role);
	}
	/// <summary> 战斗编队 </summary>
	public void Settings(int team, int position) {
		battles.ForEach(obj => obj.Settings(team, position));
	}
}