using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色 - 数据
/// </summary>
public class DataCharacter : DataAttribute {
	/// <summary> 角色名字 </summary>
	public string name;
	/// <summary> 战斗等级 </summary>
	public int level = 0;
	/// <summary> 护甲等级 </summary>
	public int armorClass = 0;
	/// <summary> 经验点 </summary>
	public Vector2Int expPoint;
	/// <summary> 生命点 </summary>
	public Vector2Int hitPoint;

	/// <summary> 基础属性 </summary>
	public DataAttribute basic = Initial();
	/// <summary> 种族 </summary>
	public DataRace race = DataRace.None();
	/// <summary> 职业 </summary>
	public DataProfession profession;
	/// <summary> 装备栏 </summary>
	public DataEquipmentSlot equipmentSlot = new DataEquipmentSlot();

	/// <summary> 力量调整值(strength) </summary>
	public int StrModifier => Modifier(Str);
	/// <summary> 敏捷调整值(dexterity) </summary>
	public int DexModifier => Modifier(Dex);
	/// <summary> 体质调整值(constitution) </summary>
	public int ConModifier => Modifier(Con);
	/// <summary> 智力调整值(intelligence) </summary>
	public int IntModifier => Modifier(Int);
	/// <summary> 感知调整值(wisdom) </summary>
	public int WisModifier => Modifier(Wis);
	/// <summary> 魅力调整值(charisma) </summary>
	public int ChaModifier => Modifier(Cha);

	public DataCharacter() {
		profession = DataProfession.None(this);
	}
	/// <summary> 更新角色状态 </summary>
	public void Update() {
		UpdateAttribute();
		// 战斗等级
		level = profession.level;
		// 护甲等级
		armorClass = GetArmorClass();
		// 升级经验 = 100 * 3^level
		expPoint.y = 100 * (int)Mathf.Pow(3, level);
		// 最大生命值
		hitPoint.y = profession.HitPoint();
	}
	/// <summary> 更新角色属性 </summary>
	public void UpdateAttribute() {
		// 更新属性
		Cover(basic);
		// 添加种族属性
		if (race != null) { Add(race); }
		// 添加职业属性
		if (profession != null) { Add(profession); }
	}
	/// <summary> 计算护甲等级（AC） </summary>
	public int GetArmorClass() {
		// TODO：需要补充专长，技能，熟练之类的加值
		return equipmentSlot.GetArmorClass(DexModifier);
	}

	// 打印角色卡
	public void PrintCharacterSheet() {
		Debug.Log($"=== {name} LV{level} ===");
		Debug.Log($"种族: {race.name}");
		Debug.Log($"职业: {profession.name}");
		Debug.Log($"力量: {Str} ({StrModifier.ToString("+#;-#;+0")})");
		Debug.Log($"敏捷: {Dex} ({DexModifier.ToString("+#;-#;+0")})");
		Debug.Log($"体质: {Con} ({ConModifier.ToString("+#;-#;+0")})");
		Debug.Log($"智力: {Int} ({IntModifier.ToString("+#;-#;+0")})");
		Debug.Log($"感知: {Wis} ({WisModifier.ToString("+#;-#;+0")})");
		Debug.Log($"魅力: {Cha} ({ChaModifier.ToString("+#;-#;+0")})");
		Debug.Log($"经验值: {expPoint}");
		Debug.Log($"生命值: {hitPoint}");
		Debug.Log($"护甲等级: {armorClass})");
	}
}
