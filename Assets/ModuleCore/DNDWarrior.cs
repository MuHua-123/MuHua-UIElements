using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DNDWarrior : MonoBehaviour {
	// 战士基础属性
	public int Strength { get; private set; }
	public int Dexterity { get; private set; }
	public int Constitution { get; private set; }
	public int Intelligence { get; private set; }
	public int Wisdom { get; private set; }
	public int Charisma { get; private set; }

	public int Level { get; private set; } = 1;
	public int HitPoints { get; private set; }
	public int ArmorClass { get; private set; }
	public string ArmorType { get; private set; } = "Chain Mail"; // 默认中甲

	private Random _random = new Random();

	private void Awake() {
		Strength = Roll4d6();
		Dexterity = Roll4d6();
		Constitution = Roll4d6();
		Intelligence = Roll4d6();
		Wisdom = Roll4d6();
		Charisma = Roll4d6();

		// 计算初始生命值（1d10+体质修正）
		HitPoints = 10 + GetAbilityModifier(Constitution);
		CalculateAC();
		PrintCharacterSheet();

		Console.WriteLine("\n=== 升级到12级 ===");
		LevelUpTo(12);
		PrintCharacterSheet();
	}

	// 4d6规则：投4次d6，去掉最低值
	private int Roll4d6() {
		List<int> rolls = new List<int>();
		for (int i = 0; i < 4; i++) {
			rolls.Add(_random.Next(1, 7));
		}
		rolls.Sort();
		return rolls[1] + rolls[2] + rolls[3]; // 取最高3个值
	}

	// 计算属性调整值（属性值-10）/2 向下取整
	private int GetAbilityModifier(int abilityScore) {
		return (int)Math.Floor((abilityScore - 10) / 2.0);
	}

	// 计算护甲等级（AC）
	private void CalculateAC() {
		int dexModifier = GetAbilityModifier(Dexterity);

		switch (ArmorType) {
			case "Plate Armor": // 重甲（敏捷修正上限+1）
				ArmorClass = 18 + Math.Min(dexModifier, 1);
				break;
			case "Chain Mail": // 中甲（敏捷修正上限+2）
				ArmorClass = 16 + Math.Min(dexModifier, 2);
				break;
			default: // 无甲（全敏捷修正）
				ArmorClass = 10 + dexModifier;
				break;
		}
	}

	// 升级到指定等级（模拟到12级）
	public void LevelUpTo(int targetLevel) {
		while (Level < targetLevel) {
			Level++;

			// 每级生命值增加：1d10+体质修正
			HitPoints += _random.Next(1, 11) + GetAbilityModifier(Constitution);

			// 每4级获得属性点（4/8/12级）
			if (Level % 4 == 0) {
				// 战士优先提升力量或体质
				if (Strength < 20) Strength += 2;
				else if (Constitution < 20) Constitution += 2;
			}

			// 6级更换板甲
			if (Level == 6) ArmorType = "Plate Armor";

			CalculateAC(); // 更新AC
		}
	}

	// 打印角色卡
	public void PrintCharacterSheet() {
		Debug.Log($"=== 战士 LV{Level} ===");
		Debug.Log($"力量: {Strength} (+{GetAbilityModifier(Strength)})");
		Debug.Log($"敏捷: {Dexterity} (+{GetAbilityModifier(Dexterity)})");
		Debug.Log($"体质: {Constitution} (+{GetAbilityModifier(Constitution)})");
		Debug.Log($"智力: {Intelligence} (+{GetAbilityModifier(Intelligence)})");
		Debug.Log($"感知: {Wisdom} (+{GetAbilityModifier(Wisdom)})");
		Debug.Log($"魅力: {Charisma} (+{GetAbilityModifier(Charisma)})");
		Debug.Log($"生命值: {HitPoints}");
		Debug.Log($"护甲: {ArmorType} (AC: {ArmorClass})");
	}
}


