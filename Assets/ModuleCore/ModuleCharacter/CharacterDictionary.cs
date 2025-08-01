using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色 - 字典
/// </summary>
public static class CharacterDictionary {
	/// <summary> 艾薇拉 精灵 法师 </summary>
	public static DataCharacter Character001() {
		DataCharacter character = new DataCharacter();
		character.name = "艾薇拉";
		character.race = RaceTool.Elven();
		character.basis = new DataAttribute { Str = 8, Dex = 12, Con = 10, Int = 16, Wis = 14, Cha = 11 };
		character.profession = ProfessionTool.Wizard();
		character.profession.Initial(character);
		character.equipment = new DataEquipment();
		// 装备法杖
		character.equipment.Wear(WeaponDictionary.Weapon302(), out DataWear old1);
		// 装备法袍
		character.equipment.Wear(ArmorDictionary.Armor101(), out DataWear old2);
		return character;
	}
	/// <summary> 托尔吉 兽人 战士 </summary>
	public static DataCharacter Character002() {
		DataCharacter character = new DataCharacter();
		character.name = "托尔吉";
		character.race = RaceTool.Orc();
		character.basis = new DataAttribute { Str = 16, Dex = 10, Con = 14, Int = 8, Wis = 10, Cha = 9 };
		character.profession = ProfessionTool.Warrior();
		character.profession.Initial(character);
		character.equipment = new DataEquipment();
		// 装备木棒和木盾
		character.equipment.Wear(WeaponDictionary.Weapon201(), out DataWear old1);
		character.equipment.Wear(WeaponDictionary.Weapon401(), out DataWear old2);
		// 装备板甲
		character.equipment.Wear(ArmorDictionary.Armor401(), out DataWear old3);
		return character;
	}
	/// <summary> 格伦布林 矮人 牧师 </summary>
	public static DataCharacter Character003() {
		DataCharacter character = new DataCharacter();
		character.name = "格伦布林";
		character.race = RaceTool.Dwarf();
		character.basis = new DataAttribute { Str = 14, Dex = 10, Con = 16, Int = 9, Wis = 15, Cha = 12 };
		character.profession = ProfessionTool.Cleric();
		character.profession.Initial(character);
		character.equipment = new DataEquipment();
		// 装备木棒和木盾
		character.equipment.Wear(WeaponDictionary.Weapon201(), out DataWear old1);
		character.equipment.Wear(WeaponDictionary.Weapon401(), out DataWear old2);
		// 装备链甲
		character.equipment.Wear(ArmorDictionary.Armor301(), out DataWear old3);
		return character;
	}
}
