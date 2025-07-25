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
		team1.Add(CharacterDictionary.Character001());
		team1.Add(CharacterDictionary.Character002());
		team1.Add(CharacterDictionary.Character003());

		team2 = new BattleTeam();
		team2.Add(MonsterDictionary.Monster001());
		team2.Add(MonsterDictionary.Monster002());
		team2.Add(MonsterDictionary.Monster002());

		battleSimulator = new BattleSimulator(team1, team2);
		battleSimulator.Transition(PhaseType.先攻阶段);
	}
}
