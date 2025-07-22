using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 模拟器 - 管理
/// </summary>
public class ManagerSimulator : ModuleSingle<ManagerSimulator> {

	/// <summary> 队伍1 </summary>
	public BattleTeam team1;
	/// <summary> 队伍2 </summary>
	public BattleTeam team2;

	public BattleSimulator battleSimulator;

	protected override void Awake() => NoReplace(false);

	private void Start() {
		team1 = new BattleTeam();
		team1.Add(RandomCharacter("艾薇拉"));
		team1.Add(RandomCharacter("托尔吉"));

		team2 = new BattleTeam();
		team2.Add(RandomCharacter("哥布林射手"));
		team2.Add(RandomCharacter("哥布林战士"));

		battleSimulator = new BattleSimulator(team1, team2);
	}
	private void Update() {
		battleSimulator.Update();
	}

	private DataCharacter RandomCharacter(string name) {
		return DataCharacter.Create(name, DataRace.Random(), DataProfession.Random());
	}
}
