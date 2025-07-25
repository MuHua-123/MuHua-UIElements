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
	public Queue<DataCombatRole> queue = new Queue<DataCombatRole>();
	/// <summary> 战斗合集 </summary>
	public List<DataCombatRole> characters = new List<DataCombatRole>();

	/// <summary> 添加角色 </summary>
	public void Add(List<DataCombatRole> list) {
		characters.AddRange(list);
	}
	/// <summary> 添加角色 </summary>
	public void Add(DataCombatRole character) {
		characters.Add(character);
	}
	/// <summary> 遍历角色 </summary>
	public void ForEach(Action<DataCombatRole> action) {
		characters.ForEach(action);
	}
	// /// <summary> 排序：大到小 </summary>
	public void OrderByDescending(Func<DataCombatRole, int> func) {
		characters = characters.OrderByDescending(func).ToList();
	}
	/// <summary> 根据条件查询元素 </summary>
	public List<DataCombatRole> Where(Func<DataCombatRole, bool> predicate) {
		return characters.Where(predicate).ToList();
	}
	/// <summary> 根据条件查询第一个匹配的元素 </summary>
	public DataCombatRole FirstOrDefault(Func<DataCombatRole, bool> predicate) {
		return characters.FirstOrDefault(predicate);
	}
	/// <summary> 更新队列 </summary>
	public void UpdateQueue() {
		queue = new Queue<DataCombatRole>(characters);
	}
	/// <summary> 取出一个 </summary>
	public bool Dequeue(out DataCombatRole battle) {
		battle = queue.Count > 0 ? queue.Dequeue() : null;
		return battle != null;
	}
}
