using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 阶段类型
/// </summary>
public enum PhaseType {
	先攻阶段, 突袭阶段, 回合阶段, 行动阶段, 结算阶段,
	选择角色, 角色攻击
}
/// <summary>
/// 阶段
/// </summary>
public interface IPhase {
	/// <summary> 执行阶段 </summary>
	public void Execute();
}
/// <summary>
/// 战斗阶段
/// </summary>
public abstract class BattlePhase : IPhase {
	/// <summary> 模拟器 </summary>
	public readonly BattleSimulator simulator;

	/// <summary> 行动角色 </summary>
	public DataCombatRole ActionRole => simulator.actionRole;
	/// <summary> 战斗队列 </summary>
	public BattleQueue BattleQueue => simulator.battleQueue;

	public BattlePhase(BattleSimulator simulator) => this.simulator = simulator;

	public abstract void Execute();
	/// <summary> 阶段过渡 </summary>
	public void Transition(PhaseType phaseType) => simulator.Transition(phaseType);
}