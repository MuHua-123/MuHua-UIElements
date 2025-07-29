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
		if (!BattleQueue.Dequeue(out simulator.actionRole)) { Transition(PhaseType.回合阶段); return; }
		// 判断是否可以行动
		if (ActionRole.hitPoint.x <= 0) { Transition(PhaseType.选择角色); return; }
		// 进行ai判断
		// TODO: 进行武器攻击，使用技能，使用法术，使用物品的判断
		Transition(PhaseType.角色攻击);
	}
}
/// <summary>
/// 武器攻击
/// </summary>
public class PhaseActionRoleAttack : BattlePhase {

	public PhaseActionRoleAttack(BattleSimulator simulator) : base(simulator) { }

	public override void Execute() {
		// 如果没有可以攻击的目标则结算战斗
		if (!AttackTarget(out DataCombatRole target)) { Transition(PhaseType.结算阶段); return; }
		// 武器判断：轻型武器使用敏捷，其他都使用力量
		string weaponName = ActionRole.weapon1.name;
		WeaponType weaponType = ActionRole.weapon1.weaponType;
		int modifier = weaponType == WeaponType.轻型武器 ? ActionRole.DexModifier : ActionRole.StrModifier;
		// 命中检定： d20 + 属性修正
		int hit = Dice.Roll20(modifier);
		int armorClass = target.armorClass;
		// 如果命中小于等于目标护甲等级，则不造成伤害
		if (hit > armorClass) {
			// 获取所有武器伤害骰
			DataDamageDice damageDice = ActionRole.weapon1.damageDice;
			// 伤害计算: 武器伤害骰 + 属性修正
			int damage = Dice.Roll(damageDice.value) + modifier;
			target.hitPoint.x -= damage;
			// 生成战斗消息
			MessageHit(hit, weaponName, target.name, armorClass, damage, damageDice.type);
		}
		else {
			// 生成战斗消息
			MessageMiss(hit, weaponName, target.name, armorClass);
		}
		// 结束行动
		Transition(PhaseType.选择角色);
	}
	/// <summary> 攻击目标 </summary>
	private bool AttackTarget(out DataCombatRole target) {
		List<DataCombatRole> roles = BattleQueue.Where(Hostility);
		if (roles.Count == 0) { target = null; return false; }
		int randomIndex = Random.Range(0, roles.Count);
		target = roles[randomIndex];
		return true;
	}
	/// <summary> 敌对目标 </summary>
	private bool Hostility(DataCombatRole role) {
		return role.team != ActionRole.team && role.hitPoint.x > 0;
	}
	/// <summary> 战斗消息：没有命中 </summary>
	private void MessageMiss(int hit, string weapon, string attacked, int armorClass) {
		MissAttack message = new MissAttack(ActionRole.name, hit, weapon, attacked, armorClass);
		Debug.Log(message);
	}
	/// <summary> 战斗消息：命中 </summary>
	private void MessageHit(int hit, string weapon, string attacked, int armorClass, int damage, DamageType damageType) {
		HitAttack message = new HitAttack(ActionRole.name, hit, weapon, attacked, armorClass, damage, damageType);
		Debug.Log(message);
	}
}