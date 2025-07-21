using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 模拟器 - 管理
/// </summary>
public class ManagerSimulator : ModuleSingle<ManagerSimulator> {

	public List<DataCharacter> cha1 = new List<DataCharacter>();
	public List<DataCharacter> cha2 = new List<DataCharacter>();
	public BattleSimulator battleSimulator;

	protected override void Awake() => NoReplace(false);

	private void Start() {
		cha1.Add(RandomCharacter("艾薇拉"));
		cha1.Add(RandomCharacter("托尔吉"));
		cha2.Add(RandomCharacter("哥布林射手"));
		cha2.Add(RandomCharacter("哥布林战士"));
		battleSimulator = new BattleSimulator(cha1, cha2);
	}
	private void Update() {
		battleSimulator.Update();
	}

	private DataCharacter RandomCharacter(string name) {
		return DataCharacter.Create(name, DataRace.Random(), DataProfession.Random());
	}
}
