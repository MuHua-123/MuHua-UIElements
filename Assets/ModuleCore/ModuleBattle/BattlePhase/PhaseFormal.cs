using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 正式阶段
/// </summary>
public class PhaseFormal : BattlePhase {
	/// <summary> 回合计数 </summary>
	public int roundCount;

	public PhaseFormal(BattleSimulator simulator) : base(simulator) { }

	public override void StartPhase() {
		// throw new System.NotImplementedException();
	}
	public override void UpdatePhase() {
		roundCount++;
		BattleQueue.UpdateQueue();
		simulator.Transition(PhaseType.行动阶段);
		// TODO：记录器
		Debug.Log($"正式回合：{roundCount}");
		string message = "存活";
		BattleQueue.ForEach(obj => message += $" {obj.name}({obj.hitPoint.x})");
		Debug.Log(message);
	}
	public override void QuitPhase() {
		// throw new System.NotImplementedException();
	}
}
