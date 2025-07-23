using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性 - 数据
/// </summary>
public class DataAttribute {
	/// <summary> 力量(strength) </summary>
	public virtual int Str { get; set; }
	/// <summary> 敏捷(dexterity) </summary>
	public virtual int Dex { get; set; }
	/// <summary> 体质(constitution) </summary>
	public virtual int Con { get; set; }
	/// <summary> 智力(intelligence) </summary>
	public virtual int Int { get; set; }
	/// <summary> 感知(wisdom) </summary>
	public virtual int Wis { get; set; }
	/// <summary> 魅力(charisma) </summary>
	public virtual int Cha { get; set; }

	/// <summary> 力量调整值(strength) </summary>
	public int StrModifier => AttributeTool.Modifier(Str);
	/// <summary> 敏捷调整值(dexterity) </summary>
	public int DexModifier => AttributeTool.Modifier(Dex);
	/// <summary> 体质调整值(constitution) </summary>
	public int ConModifier => AttributeTool.Modifier(Con);
	/// <summary> 智力调整值(intelligence) </summary>
	public int IntModifier => AttributeTool.Modifier(Int);
	/// <summary> 感知调整值(wisdom) </summary>
	public int WisModifier => AttributeTool.Modifier(Wis);
	/// <summary> 魅力调整值(charisma) </summary>
	public int ChaModifier => AttributeTool.Modifier(Cha);
}
/// <summary>
/// 角色 - 数据
/// </summary>
public class DataCharacter : DataAttribute {
	/// <summary> 名字 </summary>
	public string name;
	/// <summary> 经验 </summary>
	public int expPoint;
	/// <summary> 种族 </summary>
	public DataRace race;
	/// <summary> 属性 </summary>
	public DataAttribute basis;
	/// <summary> 职业 </summary>
	public DataProfession profession;
	/// <summary> 装备 </summary>
	public DataEquipment equipment;

	/// <summary> 战斗等级 </summary>
	public int Level => CharacterTool.GetLevel(this);
	/// <summary> 生命点 </summary>
	public int HitPoint => CharacterTool.GetHitPoint(this);
	/// <summary> 护甲等级 </summary>
	public int ArmorClass => CharacterTool.GetArmorClass(this);

	/// <summary> 力量(strength) 基础值 + 种族加值 </summary>
	public override int Str { get => basis.Str + race.Str; }
	/// <summary> 敏捷(dexterity) 基础值 + 种族加值  </summary>
	public override int Dex { get => basis.Dex + race.Dex; }
	/// <summary> 体质(constitution) 基础值 + 种族加值  </summary>
	public override int Con { get => basis.Con + race.Con; }
	/// <summary> 智力(intelligence) 基础值 + 种族加值  </summary>
	public override int Int { get => basis.Int + race.Int; }
	/// <summary> 感知(wisdom) 基础值 + 种族加值  </summary>
	public override int Wis { get => basis.Wis + race.Wis; }
	/// <summary> 魅力(charisma) 基础值 + 种族加值  </summary>
	public override int Cha { get => basis.Cha + race.Cha; }
}
/// <summary>
/// 种族 - 数据
/// </summary>
public class DataRace : DataAttribute {
	/// <summary> 种族名称 </summary>
	public string name;
}
/// <summary>
/// 职业 - 数据
/// </summary>
public class DataProfession {
	/// <summary> 职业名称 </summary>
	public string name;
	/// <summary> 生命骰子 </summary>
	public int hitDice = 0;
	/// <summary> 角色 </summary>
	public DataCharacter character;
	/// <summary> 职业等级 </summary>
	public int level = 0;
	/// <summary> 累计生命点 </summary>
	public List<int> hitPoints = new List<int>();
}