using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 阶段类型
/// </summary>
public enum PhaseType {
	先攻阶段,
	突袭阶段,
	回合阶段,
	行动阶段,
	结算阶段,
}
/// <summary>
/// 阶段
/// </summary>
public interface IPhase {
	/// <summary> 开始阶段 </summary>
	public void StartPhase();
	/// <summary> 更新阶段 </summary>
	public void UpdatePhase();
	/// <summary> 退出阶段 </summary>
	public void QuitPhase();
}
/// <summary>
/// 战斗阶段
/// </summary>
public abstract class BattlePhase : IPhase {
	/// <summary> 模拟器 </summary>
	public readonly BattleSimulator simulator;

	/// <summary> 战斗队列 </summary>
	public BattleQueue BattleQueue => simulator.battleQueue;

	public BattlePhase(BattleSimulator simulator) => this.simulator = simulator;

	public abstract void StartPhase();
	public abstract void UpdatePhase();
	public abstract void QuitPhase();
}