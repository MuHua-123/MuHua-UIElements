using System;
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
	/// <summary> 遍历角色 </summary>
	public void ForEach(Action<BattleCharacter> action) {
		characters.ForEach(action);
	}
	// /// <summary> 排序：大到小 </summary>
	public void OrderByDescending(Func<BattleCharacter, int> func) {
		characters = characters.OrderByDescending(func).ToList();
	}
	/// <summary> 更新队列 </summary>
	public void UpdateQueue() {
		queue = new Queue<BattleCharacter>(characters);
	}
	/// <summary> 取出一个 </summary>
	public bool Dequeue(out BattleCharacter battle) {
		battle = queue.Count > 0 ? queue.Dequeue() : null;
		return battle != null;
	}
}
