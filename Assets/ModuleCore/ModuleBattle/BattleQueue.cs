using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 战斗队列
/// </summary>
public class BattleQueue {
	/// <summary> 执行队列 </summary>
	public Queue<BattleCharacter> queue = new Queue<BattleCharacter>();
	/// <summary> 战斗合集 </summary>
	public List<BattleCharacter> characters = new List<BattleCharacter>();

	/// <summary> 添加角色 </summary>
	public void Add(List<BattleCharacter> list) {
		characters.AddRange(list);
	}
	/// <summary> 添加角色 </summary>
	public void Add(BattleCharacter character) {
		characters.Add(character);
	}
	/// <summary> 先攻：d20 + 敏捷调整值 </summary>
	public void Sequence() {
		characters.ForEach(obj => obj.sequence = Dice.Roll20(obj.DexModifier));
	}
	/// <summary> 排序：大到小 </summary>
	public void OrderByDescending() {
		characters = characters.OrderByDescending(c => c.sequence).ToList();
	}
	/// <summary> 回合 </summary>
	public void Reset() {
		queue = new Queue<BattleCharacter>(characters);
	}
	/// <summary> 当前行动者 </summary>
	public bool Current(out BattleCharacter battle) {
		battle = queue.Count > 0 ? queue.Dequeue() : null;
		return battle != null;
	}
}
