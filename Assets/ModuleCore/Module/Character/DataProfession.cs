using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 职业 - 数据
/// </summary>
public class DataProfession {
	/// <summary> 职业名称 </summary>
	public readonly string name;
	/// <summary> 生命骰子 </summary>
	public readonly int hitDice = 0;
	/// <summary> 角色 </summary>
	public DataCharacter character;
	/// <summary> 职业等级 </summary>
	public int level = 0;
	/// <summary> 累计生命点 </summary>
	public List<int> hitPoints = new List<int>();

	public DataProfession(string name, int hitDice) {
		this.name = name;
		this.hitDice = hitDice;
	}
	/// <summary> 初始：满生命骰子 + 体质调整值 </summary>
	public void Initial(DataCharacter character) {
		this.character = character;
		level = 1;
		// 初始生命 = 
		hitPoints = new List<int>();
		hitPoints.Add(hitDice + character.ConModifier);
	}
	/// <summary> 升级：骰生命骰子 + 体质调整值 </summary>
	public void Upgrade() {
		level++;
		int hitPoint = Dice.Roll(hitDice) + character.ConModifier;
		hitPoints.Add(hitPoint);
	}
	/// <summary> 生命点：每级生命点总和 </summary>
	public int HitPoint() {
		return hitPoints.Sum();
	}

	/// <summary> 无职业 1d4 </summary>
	public static DataProfession None() {
		return new DataProfession("无", 4);
	}
	/// <summary> 随机职业 </summary>
	public static DataProfession Random() {
		int index = Dice.Roll(5);
		if (index == 1) { return Warrior(); }
		if (index == 2) { return Wizard(); }
		if (index == 3) { return Cleric(); }
		if (index == 4) { return Ranger(); }
		if (index == 5) { return Chanter(); }
		return None();
	}
	/// <summary> 战士 1d10 </summary>
	public static DataProfession Warrior() {
		return new DataProfession("战士", 10);
	}
	/// <summary> 法师 1d6 </summary>
	public static DataProfession Wizard() {
		return new DataProfession("法师", 6);
	}
	/// <summary> 牧师 1d8 </summary>
	public static DataProfession Cleric() {
		return new DataProfession("牧师", 8);
	}
	/// <summary> 游侠 1d8 </summary>
	public static DataProfession Ranger() {
		return new DataProfession("游侠", 8);
	}
	/// <summary> 歌者 1d6 </summary>
	public static DataProfession Chanter() {
		return new DataProfession("歌者", 6);
	}
}
