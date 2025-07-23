using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 先攻阶段
/// </summary>
public class PhaseInitiative : BattlePhase {

	public PhaseInitiative(BattleSimulator simulator) : base(simulator) { }

	public override void StartPhase() {
		// throw new System.NotImplementedException();
	}
	public override void UpdatePhase() {
		BattleQueue.ForEach(obj => obj.sequence = Dice.Roll20(obj.DexModifier));
		BattleQueue.OrderByDescending(c => c.sequence);
		// TODO：需要添加突袭阶段
		simulator.Transition(PhaseType.回合阶段);
		// TODO：记录器
		string message = "先攻";
		BattleQueue.ForEach(obj => message += $" {obj.name}({obj.sequence})");
		Debug.Log(message);
	}
	public override void QuitPhase() {
		// throw new System.NotImplementedException();
	}
}
