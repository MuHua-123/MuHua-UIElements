using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 种族 - 工具
/// </summary>
public static class ProfessionTool {

	#region 扩展
	/// <summary> 初始：满生命骰子 + 体质调整值 </summary>
	public static void Initial(this DataProfession profession, DataCharacter character) {
		profession.character = character;
		int hitPoint = profession.hitDice + character.ConModifier;
		profession.level = 1;
		profession.hitPoints = new List<int> { hitPoint };
	}
	/// <summary> 升级：骰生命骰子 + 体质调整值 </summary>
	public static void Upgrade(this DataProfession profession) {
		profession.level++;
		int hitPoint = Dice.Roll(profession.hitDice);
		int modifier = profession.character.ConModifier;
		profession.hitPoints.Add(hitPoint + modifier);
	}
	/// <summary> 生命点：每级生命点总和 </summary>
	public static int HitPoint(this DataProfession profession) {
		return profession.hitPoints.Sum();
	}
	#endregion

	#region 创建
	/// <summary> 创建 </summary>
	public static DataProfession Create(string name, int hitDice) {
		DataProfession profession = new DataProfession { name = name, hitDice = hitDice };
		profession.name = name;
		profession.hitDice = hitDice;
		return profession;
	}
	/// <summary> 无职业 1d4 </summary>
	public static DataProfession None() {
		return Create("无", 4);
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
		return Create("战士", 10);
	}
	/// <summary> 法师 1d6 </summary>
	public static DataProfession Wizard() {
		return Create("法师", 6);
	}
	/// <summary> 牧师 1d8 </summary>
	public static DataProfession Cleric() {
		return Create("牧师", 8);
	}
	/// <summary> 游侠 1d8 </summary>
	public static DataProfession Ranger() {
		return Create("游侠", 8);
	}
	/// <summary> 歌者 1d6 </summary>
	public static DataProfession Chanter() {
		return Create("歌者", 6);
	}
	#endregion
}
