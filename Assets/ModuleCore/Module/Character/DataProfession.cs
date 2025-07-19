using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 职业 - 数据
/// </summary>
public class DataProfession : DataAttribute {
	/// <summary> 绑定角色 </summary>
	public readonly DataCharacter character;
	/// <summary> 职业名称 </summary>
	public readonly string name;
	/// <summary> 生命骰子 </summary>
	public readonly int hitDice = 0;
	/// <summary> 职业等级 </summary>
	public int level = 0;
	/// <summary> 累计生命点 </summary>
	public List<int> hitPoints = new List<int>();

	public DataProfession(DataCharacter character, string name, int hitDice) {
		this.character = character;
		this.name = name;
		this.hitDice = hitDice;

		character.UpdateAttribute();
		level = 1;
		hitPoints = new List<int>();
		// 初始生命 = 满生命骰子 + 体质调整值
		hitPoints.Add(hitDice + character.ConModifier);
	}
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

	/// <summary> 无职业 1d4 </summary>
	public static DataProfession None(DataCharacter character) {
		return new DataProfession(character, "无", 4);
	}
	/// <summary> 随机职业 </summary>
	public static DataProfession Random(DataCharacter character) {
		int index = Dice.Roll(5);
		if (index == 1) { return Warrior(character); }
		if (index == 2) { return Wizard(character); }
		if (index == 3) { return Cleric(character); }
		if (index == 4) { return Ranger(character); }
		if (index == 5) { return Chanter(character); }
		return None(character);
	}
	/// <summary> 战士 1d10 </summary>
	public static DataProfession Warrior(DataCharacter character) {
		return new DataProfession(character, "战士", 10);
	}
	/// <summary> 法师 1d6 </summary>
	public static DataProfession Wizard(DataCharacter character) {
		return new DataProfession(character, "法师", 6);
	}
	/// <summary> 牧师 1d8 </summary>
	public static DataProfession Cleric(DataCharacter character) {
		return new DataProfession(character, "牧师", 8);
	}
	/// <summary> 游侠 1d8 </summary>
	public static DataProfession Ranger(DataCharacter character) {
		return new DataProfession(character, "游侠", 8);
	}
	/// <summary> 歌者 1d6 </summary>
	public static DataProfession Chanter(DataCharacter character) {
		return new DataProfession(character, "歌者", 6);
	}
}
