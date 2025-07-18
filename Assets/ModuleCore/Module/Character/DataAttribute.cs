using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	/// <summary> 创建初始属性 </summary>
	public static DataAttribute Initial() {
		DataAttribute attribute = new DataAttribute();
		attribute.Str = Dice.RollAttribute();
		attribute.Dex = Dice.RollAttribute();
		attribute.Con = Dice.RollAttribute();
		attribute.Int = Dice.RollAttribute();
		attribute.Wis = Dice.RollAttribute();
		attribute.Cha = Dice.RollAttribute();
		return attribute;
	}
	// 计算属性调整值（属性值-10）/2 向下取整
	public int Modifier(int value) {
		return (int)System.Math.Floor((value - 10) / 2.0);
	}
	/// <summary> 添加属性 </summary>
	public void Add(DataAttribute value) {
		Str += value.Str;
		Dex += value.Dex;
		Con += value.Con;
		Int += value.Int;
		Wis += value.Wis;
		Cha += value.Cha;
	}
	/// <summary> 减少属性 </summary>
	public void Sub(DataAttribute value) {
		Str -= value.Str;
		Dex -= value.Dex;
		Con -= value.Con;
		Int -= value.Int;
		Wis -= value.Wis;
		Cha -= value.Cha;
	}
	/// <summary> 覆盖属性 </summary>
	public void Cover(DataAttribute value) {
		Str = value.Str;
		Dex = value.Dex;
		Con = value.Con;
		Int = value.Int;
		Wis = value.Wis;
		Cha = value.Cha;
	}
}
