using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 种族 - 数据
/// </summary>
public class DataRace : DataAttribute {
	/// <summary> 种族名称 </summary>
	public readonly string name;

	public DataRace(string name) {
		this.name = name;
	}

	// 混血人类，木精灵，丘陵矮人(Hill Dwarf)，龙裔，半精灵
	/// <summary> 无种族 </summary>
	public static DataRace None() {
		return new DataRace("未知");
	}
	/// <summary> 随机职业 </summary>
	public static DataRace Random() {
		int index = Dice.Roll(5);
		if (index == 1) { return Human(); }
		if (index == 2) { return Elven(); }
		if (index == 3) { return Dwarf(); }
		if (index == 4) { return Orc(); }
		if (index == 5) { return Halfling(); }
		return None();
	}
	/// <summary> 人类(Human) </summary>
	public static DataRace Human() {
		DataRace race = new DataRace("人类");
		// 种族属性加成 全属性+1
		race.Str = 1; race.Dex = 1; race.Con = 1;
		race.Int = 1; race.Wis = 1; race.Cha = 1;
		return race;
	}
	/// <summary> 精灵(Elven) </summary>
	public static DataRace Elven() {
		DataRace race = new DataRace("精灵");
		// 种族属性加成 敏捷+2 智力+2 感知+1
		race.Dex = 2; race.Int = 2; race.Int = 1;
		return race;
	}
	/// <summary> 矮人(Dwarf) </summary>
	public static DataRace Dwarf() {
		DataRace race = new DataRace("矮人");
		// 种族属性加成 力量+2 体质+2 感知+1
		race.Str = 2; race.Con = 2; race.Wis = 1;
		return race;
	}
	/// <summary> 半兽人(Orc) </summary>
	public static DataRace Orc() {
		DataRace race = new DataRace("半兽人");
		// 种族属性加成 力量+3 体质+2
		race.Str = 3; race.Con = 2;
		return race;
	}
	/// <summary> 半身人(Halfling) </summary>
	public static DataRace Halfling() {
		DataRace race = new DataRace("半身人");
		// 种族属性加成 敏捷+2 魅力+2 体质+1
		race.Dex = 2; race.Cha = 2; race.Con = 1;
		return race;
	}
}
