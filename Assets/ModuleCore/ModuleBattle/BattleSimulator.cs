using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 战斗 - 模拟器
/// </summary>
public class BattleSimulator {
	/// <summary> 当前行动 </summary>
	public DataCombatRole actionRole;
	/// <summary> 战斗队列 </summary>
	public BattleQueue battleQueue = new BattleQueue();

	/// <summary> 当前阶段 </summary>
	public IPhase currentPhase;
	/// <summary> 阶段字典 </summary>
	public Dictionary<PhaseType, IPhase> dictionary = new Dictionary<PhaseType, IPhase>();

	public BattleSimulator(BattleTeam team1, BattleTeam team2) {
		team1.Initial();
		team2.Initial();

		team1.Settings(1, 1);
		team2.Settings(2, 1);

		battleQueue.Add(team1.battles);
		battleQueue.Add(team2.battles);

		dictionary.Add(PhaseType.先攻阶段, new PhaseInitiative(this));
		dictionary.Add(PhaseType.突袭阶段, new PhaseAssault(this));
		dictionary.Add(PhaseType.回合阶段, new PhaseFormal(this));
		dictionary.Add(PhaseType.行动阶段, new PhaseAction(this));
		dictionary.Add(PhaseType.选择角色, new PhaseActionRoleSelect(this));
		dictionary.Add(PhaseType.角色攻击, new PhaseActionRoleAttack(this));
		dictionary.Add(PhaseType.结算阶段, new PhaseFinish(this));
	}

	/// <summary> 阶段过渡 </summary>
	public void Transition(PhaseType phaseType) {
		// 检查阶段字典中是否存在指定的阶段类型
		if (!dictionary.TryGetValue(phaseType, out IPhase newPhase)) { return; }
		currentPhase = newPhase;
		currentPhase?.Execute();
	}
}
