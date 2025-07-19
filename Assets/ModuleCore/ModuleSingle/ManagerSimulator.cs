using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 模拟器 - 管理
/// </summary>
public class ManagerSimulator : ModuleSingle<ManagerSimulator> {

	public DataCharacter character;

	protected override void Awake() => NoReplace(false);

	private void Start() {
		character = new DataCharacter();
		// 种族
		character.race = DataRace.Orc();
		// 职业
		character.profession = DataProfession.Warrior(character);
		// 更新属性
		character.Update();
		// 打印角色卡
		character.PrintCharacterSheet();
	}
}
