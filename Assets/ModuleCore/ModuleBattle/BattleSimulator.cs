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

	/// <summary> 战斗队列 </summary>
	public BattleQueue battleQueue = new BattleQueue();

	/// <summary> 当前阶段 </summary>
	public IPhase currentPhase;
	/// <summary> 阶段字典 </summary>
	public Dictionary<PhaseType, IPhase> dictionary = new Dictionary<PhaseType, IPhase>();

	public BattleSimulator(BattleTeam team1, BattleTeam team2) {
		battleQueue.Add(team1.battles);
		battleQueue.Add(team2.battles);

		dictionary.Add(PhaseType.先攻阶段, new PhaseInitiative(this));
		dictionary.Add(PhaseType.突袭阶段, new PhaseAssault(this));
		dictionary.Add(PhaseType.回合阶段, new PhaseFormal(this));
		dictionary.Add(PhaseType.行动阶段, new PhaseAction(this));
		dictionary.Add(PhaseType.结算阶段, new PhaseFinish(this));
	}
	public void Update() {
		currentPhase?.UpdatePhase();
	}

	/// <summary> 阶段过渡 </summary>
	public void Transition(PhaseType phaseType) {
		// 检查阶段字典中是否存在指定的阶段类型
		if (dictionary.TryGetValue(phaseType, out IPhase newPhase)) {
			// 如果存在则更新当前阶段
			currentPhase?.QuitPhase();
			currentPhase = newPhase;
			currentPhase?.StartPhase();
			Debug.Log($"战斗阶段已转换为: {phaseType}");
		}
		else {
			// 不存在时输出警告信息
			Debug.LogWarning($"阶段字典中不存在 {phaseType} 对应的阶段实现");
		}
	}
}
