using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 行动阶段
/// </summary>
public class PhaseAction : BattlePhase {
	/// <summary> 当前行动 </summary>
	private BattleCharacter currentAction;
	/// <summary> 当前目标 </summary>
	private BattleCharacter currentTarget;

	public PhaseAction(BattleSimulator simulator) : base(simulator) { }

	public override void StartPhase() {
		// throw new System.NotImplementedException();
	}
	public override void UpdatePhase() {
		// 选择行动的角色，如果没有则进入下一轮
		if (!SelectAction()) { return; }
		// 选择攻击的目标，如果没有目标则结算战斗
		if (!SelectTarget()) { return; }
		// 命中检定
		int hit = Dice.Roll20(currentAction.DexModifier);
		int ac = currentTarget.armorClass;
		bool isHit = hit > ac;
		// 伤害计算
		if (isHit) {
			int damage = Dice.Roll8(currentAction.StrModifier);
			currentTarget.hitPoint.x -= damage;
			Debug.Log($"{currentAction.name}使用 普通攻击({hit}) 对 {currentTarget.name}({ac}) 造成 {damage} 点伤害！");
		}
		else {
			Debug.Log($"{currentAction.name}使用 普通攻击({hit}) 对 {currentTarget.name}({ac}) 未命中！");
		}
		// TODO：记录器
		// Debug.Log($"正式回合：{roundCount}");
	}
	public override void QuitPhase() {
		// throw new System.NotImplementedException();
	}

	/// <summary> 选择当前行动角色 </summary>
	private bool SelectAction() {
		// 选择行动的角色，如果没有则进入下一回合
		if (!BattleQueue.Dequeue(out currentAction)) { simulator.Transition(PhaseType.回合阶段); return false; }
		// 判断是否可以行动
		if (!currentAction.IsAction()) { return false; }
		return true;
	}
	/// <summary> 选择当前目标角色 </summary>
	private bool SelectTarget() {
		// 选择一个可以攻击的目标
		currentTarget = BattleQueue.FirstOrDefault(obj => currentAction.IsHostility(obj));
		// 如果没有可以攻击的目标则结算战斗
		if (currentTarget == null) { simulator.Transition(PhaseType.结算阶段); }
		return currentTarget != null;
	}
}
