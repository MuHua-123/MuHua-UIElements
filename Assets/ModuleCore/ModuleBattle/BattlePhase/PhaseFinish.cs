using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 结束阶段
/// </summary>
public class PhaseFinish : BattlePhase {

	public PhaseFinish(BattleSimulator simulator) : base(simulator) { }

	public override void StartPhase() {
		// TODO：记录器
		Debug.Log("结束战斗!");
	}
	public override void UpdatePhase() {
		// TODO：需要添加结算判断
		// simulator.Transition(PhaseType.回合阶段);
		// TODO：记录器

	}
	public override void QuitPhase() {
		// throw new System.NotImplementedException();
	}
}
