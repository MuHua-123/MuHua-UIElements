using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 行动阶段
/// </summary>
public class PhaseAction : BattlePhase {

	public PhaseAction(BattleSimulator simulator) : base(simulator) { }

	public override void Execute() {

	}
}
/// <summary>
/// 行动角色选择
/// </summary>
public class PhaseActionRoleSelect : BattlePhase {

	public PhaseActionRoleSelect(BattleSimulator simulator) : base(simulator) { }

	public override void Execute() {
		// 选择行动的角色，如果没有则进入下一回合
		PhaseType phase = BattleQueue.Dequeue(out simulator.actionRole) ? PhaseType.角色攻击 : PhaseType.回合阶段;
		simulator.Transition(phase);
	}
}
/// <summary>
/// 行动角色攻击
/// </summary>
public class PhaseActionRoleAttack : BattlePhase {

	public PhaseActionRoleAttack(BattleSimulator simulator) : base(simulator) { }

	public override void Execute() {
		// 判断是否可以行动
		if (ActionRole.hitPoint.x <= 0) { simulator.Transition(PhaseType.选择角色); return; }
		// 选择可以攻击的目标
		List<DataCombatRole> roles = AttackTarget();
		// 如果没有可以攻击的目标则结算战斗
		if (roles.Count == 0) { simulator.Transition(PhaseType.结算阶段); return; }
		// 攻击单体目标
		int randomIndex = Random.Range(0, roles.Count);
		DataCombatRole target = roles[randomIndex];
		// 武器判断
		// ActionRole.weapon1
		// 命中检定
		int hit = Dice.Roll20(ActionRole.StrModifier);
		int armorClass = target.armorClass;
		// 伤害计算
		int damage = Dice.Roll8(ActionRole.StrModifier);
		if (hit > armorClass) { target.hitPoint.x -= damage; }
		// 生成战斗消息
		MessageNormalAttack message = new MessageNormalAttack();
		message.Settings(ActionRole, hit, damage);
		message.Settings(target, armorClass);
		Debug.Log(message);
		simulator.Transition(PhaseType.选择角色);
	}
	/// <summary> 攻击目标 </summary>
	private List<DataCombatRole> AttackTarget() {
		return BattleQueue.Where(Hostility);
	}
	/// <summary> 敌对目标 </summary>
	public bool Hostility(DataCombatRole role) {
		return role.team != ActionRole.team && role.hitPoint.x > 0;
	}
}
/// <summary>
/// 战斗行动
/// </summary>
public class BattleAction {

}