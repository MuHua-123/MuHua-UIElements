using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 行动阶段
/// </summary>
public class PhaseAction : BattlePhase {

	public PhaseAction(BattleSimulator simulator) : base(simulator) { }

	public override void StartPhase() {
		// throw new System.NotImplementedException();
	}
	public override void UpdatePhase() {
		// 选择行动目标，没有行动目标则进入结算阶段
		if (!BattleQueue.Dequeue(out BattleCharacter character)) { simulator.Transition(PhaseType.结算阶段); return; }
		// 判断是否可以行动

		// 选择一个目标

		// 对目标进行攻击

		// TODO：记录器
		// Debug.Log($"正式回合：{roundCount}");
	}
	public override void QuitPhase() {
		// throw new System.NotImplementedException();
	}
}
