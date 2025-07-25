using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗报告
/// </summary>
public class BattleReport {
	/// <summary> 战斗消息 </summary>
	public List<BattleMessage> messages;
}
/// <summary>
/// 战斗消息
/// </summary>
public class BattleMessage {

}
/// <summary>
/// 普通攻击 - 战斗消息
/// </summary>
public class MessageNormalAttack : BattleMessage {
	/// <summary> 攻击者 </summary>
	public DataCombatRole attacker;
	/// <summary> 命中值 </summary>
	public int hit;
	/// <summary> 伤害值 </summary>
	public int damage;

	/// <summary> 被攻击者 </summary>
	public DataCombatRole attacked;
	/// <summary> 护甲等级 </summary>
	public int armorClass;

	/// <summary> 设置攻击者 </summary>
	public void Settings(DataCombatRole attacker, int hit, int damage) {
		this.attacker = attacker;
		this.hit = hit;
		this.damage = damage;
	}
	/// <summary> 设置被攻击者 </summary>
	public void Settings(DataCombatRole attacked, int armorClass) {
		this.attacked = attacked;
		this.armorClass = armorClass;
	}
	public override string ToString() {
		return $"{attacker.name}{Dynamics()}{attacked.name}({armorClass})，{attacked.name}{Result()}";
	}

	/// <summary> 动态修饰词 </summary>
	private string Dynamics() {
		return $"使用长剑劈向({hit})";
	}
	/// <summary> 结果修饰词 </summary>
	private string Result() {
		return hit <= armorClass ? Miss() : Hit();
	}
	// 未命中的多种情况
	private string Miss() {
		string[] missText = new string[] {
			"轻松避开了这一击！",
			"巧妙地闪开了！",
			"险之又险地躲过了！",
			"成功格挡住了！"
		};
		return RandomChoice(missText);
	}
	// 命中的情况，可根据伤害量或是否暴击细分
	private string Hit() {
		string[] hitText = new string[] {
			$"躲避不及遭受到了{damage}点伤害！",
			$"被狠狠击中，承受了{damage}点伤害！",
			$"格挡失败受到了{damage}点伤害！",
		};
		return RandomChoice(hitText);
	}
	// 辅助方法：随机选择一个字符串，增加多样性
	private string RandomChoice(params string[] options) {
		if (options.Length == 0) return "";
		return options[Random.Range(0, options.Length)];
	}
}