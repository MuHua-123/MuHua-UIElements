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
		character.Update();
		character.PrintCharacterSheet();
	}
}
