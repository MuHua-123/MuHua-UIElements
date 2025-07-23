using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色工具
/// </summary>
public static class CharacterTool {

	#region 设置参数
	/// <summary> 设置职业 </summary>
	public static void Settings(this DataCharacter character, DataRace race) {
		character.race = race;
	}
	/// <summary> 设置职业 </summary>
	public static void Settings(this DataCharacter character, DataProfession profession) {
		character.profession = profession;
		profession.character = character;
	}
	#endregion

	#region 属性扩展
	/// <summary> 战斗等级 </summary>
	public static int GetLevel(this DataCharacter character) {
		// TODO：需要补充多职业的等级总和
		return character.profession.level;
	}
	/// <summary> 生命点 </summary>
	public static int GetHitPoint(this DataCharacter character) {
		// TODO：需要补充多职业的生命值加成
		return character.profession.HitPoint();
	}
	/// <summary> 计算护甲等级（AC） </summary>
	public static int GetArmorClass(this DataCharacter character) {
		// TODO：需要补充专长，技能，熟练之类的加值
		int modifier = character.DexModifier;
		return character.equipment.ArmorClass(modifier);
	}
	#endregion

	#region 创建角色
	/// <summary> 创建默认角色 </summary>
	public static DataCharacter Create(string name) {
		DataCharacter character = new DataCharacter();
		character.name = name;
		character.race = RaceTool.None();
		character.basis = AttributeTool.Random();
		character.profession = ProfessionTool.None();
		character.profession.Initial(character);
		character.equipment = new DataEquipment();
		return character;
	}
	/// <summary> 创建默认角色 </summary>
	public static DataCharacter Create(string name, DataRace race, DataProfession profession) {
		DataCharacter character = new DataCharacter();
		character.name = name;
		character.race = race;
		character.basis = AttributeTool.Random();
		character.profession = profession;
		character.profession.Initial(character);
		character.equipment = new DataEquipment();
		return character;
	}
	#endregion

	// 打印角色卡
	public static void PrintCharacterSheet(this DataCharacter character) {
		Debug.Log($"=== {character.name} LV{character.Level} ===");
		Debug.Log($"种族: {character.race.name}");
		Debug.Log($"职业: {character.profession.name}");
		Debug.Log($"力量: {character.Str} ({character.StrModifier.ToString("+#;-#;+0")})");
		Debug.Log($"敏捷: {character.Dex} ({character.DexModifier.ToString("+#;-#;+0")})");
		Debug.Log($"体质: {character.Con} ({character.ConModifier.ToString("+#;-#;+0")})");
		Debug.Log($"智力: {character.Int} ({character.IntModifier.ToString("+#;-#;+0")})");
		Debug.Log($"感知: {character.Wis} ({character.WisModifier.ToString("+#;-#;+0")})");
		Debug.Log($"魅力: {character.Cha} ({character.ChaModifier.ToString("+#;-#;+0")})");
		Debug.Log($"经验值: {character.expPoint}");
		Debug.Log($"生命值: {character.HitPoint}");
		Debug.Log($"护甲等级: {character.ArmorClass})");
	}
}
