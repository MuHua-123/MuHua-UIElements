using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 战斗 - 模拟器
/// </summary>
public class BattleSimulator {

	// 先攻判断
	// 艾薇拉(18) → 哥布林射手(15) → 托尔吉(7) → 哥布林战士(5)

	// 突袭轮
	// 哥布林射手 使用短弓射击（23）对艾薇拉（AC12）造成了 5穿刺伤害

	// 正式回合1
	// ‌艾薇拉 施放纠缠术‌（法术豁免DC14），哥布林射手（敏捷豁免6）被束缚
	// ‌艾薇拉 躲至矿石掩体后（AC提升至14）

	// 哥布林射手 试图挣脱藤蔓（力量豁免‌3‌）失败了
	// 哥布林射手 使用多重射击，第一箭（劣势2）对托尔吉（AC14）未命中，第二箭（劣势9）对托尔吉（AC14）未命中

	// 托尔吉 使用狂暴‌（附赠动作开启）→ 获得力量加成与抗性
	// 托尔吉 移动至哥布林战士前面
	// 托尔吉 使用巨斧猛击（19）对哥布林战士（AC12） 造成了 14钝击伤害

	// 哥布林战士死亡

	// 正式回合2
	// ‌艾薇拉 施放奥术飞弹‌（法术豁免DC14），哥布林射手（敏捷豁免6）受到了 10奥术伤害

	// 哥布林射手死亡

	/// <summary> 回合计数 </summary>
	public int roundCount;
	/// <summary> 行动间隔 </summary>
	public float interval;
	/// <summary> 最大间隔 </summary>
	public float maxInterval = 1f;
	/// <summary> 当前行动 </summary>
	public Action currentAction;
	/// <summary> 队伍1 </summary>
	public BattleTeam team1;
	/// <summary> 队伍2 </summary>
	public BattleTeam team2;
	/// <summary> 战斗队列 </summary>
	public BattleQueue battleQueue = new BattleQueue();

	public BattleSimulator(List<DataCharacter> cha1, List<DataCharacter> cha2) {
		List<BattleCharacter> bCha1 = new List<BattleCharacter>();
		cha1.ForEach(obj => bCha1.Add(new BattleCharacter(obj)));
		List<BattleCharacter> bCha2 = new List<BattleCharacter>();
		cha2.ForEach(obj => bCha2.Add(new BattleCharacter(obj)));

		team1 = new BattleTeam(bCha1);
		team2 = new BattleTeam(bCha2);

		battleQueue.Add(bCha1);
		battleQueue.Add(bCha2);
		battleQueue.Sequence();
		battleQueue.OrderByDescending();

		currentAction = UpdateRound;
	}
	public void Update() {
		if (interval > 0) { interval -= Time.deltaTime; return; }
		currentAction?.Invoke();
	}

	/// <summary> 更新回合 </summary>
	public void UpdateRound() {
		roundCount++;
		battleQueue.Reset();
		Debug.Log($"正式回合 {roundCount}");

		interval = maxInterval;
		currentAction = SelectActionTarget;
	}
	/// <summary> 选择行动对象 </summary>
	public void SelectActionTarget() {
		if (!battleQueue.Current(out BattleCharacter character)) {
			currentAction = UpdateRound; return;
		}
		Debug.Log($"当前 {character.name}({character.sequence}) 行动");

		interval = maxInterval;
		currentAction = SelectActionTarget;
	}
	public void SelectAttackTarget(BattleCharacter character) {

	}
}
/// <summary>
/// 战斗队伍
/// </summary>
public class BattleTeam {

	public List<BattleCharacter> characters;

	public BattleTeam(List<BattleCharacter> characters) {
		this.characters = characters;
	}
}