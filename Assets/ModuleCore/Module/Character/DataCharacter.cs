using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色 - 数据
/// </summary>
public class DataCharacter : DataAttribute {
	/// <summary> 角色名字 </summary>
	public string name;
	/// <summary> 经验点 </summary>
	public int expPoint;
	/// <summary> 种族 </summary>
	public DataRace race;
	/// <summary> 属性 </summary>
	public DataAttribute basis;
	/// <summary> 职业 </summary>
	public DataProfession profession;
	/// <summary> 装备栏 </summary>
	public DataEquipmentSlot equipmentSlot;

	/// <summary> 战斗等级 </summary>
	public int Level => GetLevel();
	/// <summary> 生命点 </summary>
	public int HitPoint => GetHitPoint();
	/// <summary> 护甲等级 </summary>
	public int ArmorClass => GetArmorClass();

	public override int Str { get => basis.Str + race.Str; }
	public override int Dex { get => basis.Dex + race.Dex; }
	public override int Con { get => basis.Con + race.Con; }
	public override int Int { get => basis.Int + race.Int; }
	public override int Wis { get => basis.Wis + race.Wis; }
	public override int Cha { get => basis.Cha + race.Cha; }

	/// <summary> 设置职业 </summary>
	public void Settings(DataRace race) {
		this.race = race;
	}
	/// <summary> 设置职业 </summary>
	public void Settings(DataProfession profession) {
		this.profession = profession;
		profession.character = this;
	}
	/// <summary> 战斗等级 </summary>
	public int GetLevel() {
		// TODO：需要补充多职业的等级总和
		return profession.level;
	}
	/// <summary> 生命点 </summary>
	public int GetHitPoint() {
		// TODO：需要补充多职业的生命值加成
		return profession.HitPoint();
	}
	/// <summary> 计算护甲等级（AC） </summary>
	public int GetArmorClass() {
		// TODO：需要补充专长，技能，熟练之类的加值
		return equipmentSlot.GetArmorClass(DexModifier);
	}

	/// <summary> 创建默认角色 </summary>
	public static DataCharacter Create(string name) {
		DataCharacter character = new DataCharacter();
		character.name = name;
		character.race = DataRace.None();
		character.basis = Initial();
		character.profession = DataProfession.None();
		character.profession.Initial(character);
		character.equipmentSlot = new DataEquipmentSlot();
		return character;
	}
	/// <summary> 创建默认角色 </summary>
	public static DataCharacter Create(string name, DataRace race, DataProfession profession) {
		DataCharacter character = new DataCharacter();
		character.name = name;
		character.race = race;
		character.basis = Initial();
		character.profession = profession;
		character.profession.Initial(character);
		character.equipmentSlot = new DataEquipmentSlot();
		return character;
	}

	// 打印角色卡
	public void PrintCharacterSheet() {
		Debug.Log($"=== {name} LV{Level} ===");
		Debug.Log($"种族: {race.name}");
		Debug.Log($"职业: {profession.name}");
		Debug.Log($"力量: {Str} ({StrModifier.ToString("+#;-#;+0")})");
		Debug.Log($"敏捷: {Dex} ({DexModifier.ToString("+#;-#;+0")})");
		Debug.Log($"体质: {Con} ({ConModifier.ToString("+#;-#;+0")})");
		Debug.Log($"智力: {Int} ({IntModifier.ToString("+#;-#;+0")})");
		Debug.Log($"感知: {Wis} ({WisModifier.ToString("+#;-#;+0")})");
		Debug.Log($"魅力: {Cha} ({ChaModifier.ToString("+#;-#;+0")})");
		Debug.Log($"经验值: {expPoint}");
		Debug.Log($"生命值: {HitPoint}");
		Debug.Log($"护甲等级: {ArmorClass})");
	}
}
