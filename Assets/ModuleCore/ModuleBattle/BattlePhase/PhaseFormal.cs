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

	public override void Execute() {
		roundCount++;
		BattleQueue.UpdateQueue();
		// TODO：记录器
		Debug.Log($"正式回合：{roundCount}");
		string message = "存活";
		BattleQueue.ForEach(obj => message += $" {obj.name}({obj.hitPoint.x})");
		Debug.Log(message);
		simulator.Transition(PhaseType.选择角色);
	}
}
