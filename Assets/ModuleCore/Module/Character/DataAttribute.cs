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
