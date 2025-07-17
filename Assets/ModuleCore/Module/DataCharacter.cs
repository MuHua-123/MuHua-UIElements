using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色 - 数据
/// </summary>
public class DataCharacter {
	/// <summary> 角色名字 </summary>
	public string name;
	/// <summary> 基础属性 </summary>
	public DataAttribute basic;
	/// <summary> 角色职业 </summary>
	public DataProfession profession;
}
/// <summary>
/// 属性 - 数据
/// </summary>
public class DataAttribute {
	/// <summary> 力量(strength) </summary>
	public int Str;
	/// <summary> 敏捷(dexterity) </summary>
	public int Dex;
	/// <summary> 体质(constitution) </summary>
	public int Con;
	/// <summary> 智力(intelligence) </summary>
	public int Int;
	/// <summary> 感知(wisdom) </summary>
	public int Wis;
	/// <summary> 魅力(charisma) </summary>
	public int Cha;
}
/// <summary>
/// 职业 - 数据
/// </summary>
public class DataProfession : DataAttribute {

}