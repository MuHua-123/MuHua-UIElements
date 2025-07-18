using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 职业 - 数据
/// </summary>
public class DataProfession : DataAttribute {
	/// <summary> 职业名称 </summary>
	public string name;
	/// <summary> 职业等级 </summary>
	public int level = 0;
	/// <summary> 生命骰子 </summary>
	public int hitDice = 0;
	/// <summary> 累计生命点 </summary>
	public List<int> hitPoints = new List<int>();

	/// <summary> 升级：骰生命骰子+体质调整值 </summary>
	public void Upgrade(int modifier) {
		level++;
		int hitPoint = Dice.Roll(hitDice) + modifier;
		hitPoints.Add(hitPoint);
	}
	/// <summary> 生命点：每级生命点总和 </summary>
	public int HitPoint() {
		return hitPoints.Sum();
	}

	/// <summary> 无职业 </summary>
	public static DataProfession None() {
		return new DataProfession() { name = "无" };
	}
	/// <summary> 战士职业 </summary>
	public static DataProfession Warrior(int modifier) {
		DataProfession profession = new DataProfession();
		//  战士，初始1级，1d10生命骰子，初始生命=满生命骰子+体质调整值
		profession.name = "战士";
		profession.level = 1;
		profession.hitDice = 10;
		profession.hitPoints.Add(profession.hitDice + modifier);
		// 职业属性加成
		profession.Str = 2;
		profession.Con = 1;
		return profession;
	}
	/// <summary> 法师职业 </summary>
	public static DataProfession Wizard(int modifier) {
		DataProfession profession = new DataProfession();
		//  法师，初始1级，1d6生命骰子，初始生命=满生命骰子+体质调整值
		profession.name = "法师";
		profession.level = 1;
		profession.hitDice = 6;
		profession.hitPoints.Add(profession.hitDice + modifier);
		// 职业属性加成
		profession.Int = 2;
		profession.Dex = 1;
		return profession;
	}
	/// <summary> 牧师职业 </summary>
	public static DataProfession Cleric(int modifier) {
		DataProfession profession = new DataProfession();
		//  牧师，初始1级，1d8生命骰子，初始生命=满生命骰子+体质调整值
		profession.name = "牧师";
		profession.level = 1;
		profession.hitDice = 8;
		profession.hitPoints.Add(profession.hitDice + modifier);
		// 职业属性加成
		profession.Wis = 2;
		profession.Con = 1;
		return profession;
	}
}
