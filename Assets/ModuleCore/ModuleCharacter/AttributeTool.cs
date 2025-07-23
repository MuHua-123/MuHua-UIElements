using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 属性 - 工具
/// </summary>
public static class AttributeTool {

	#region  创建
	/// <summary> 随机初始属性 </summary>
	public static DataAttribute Random() {
		DataAttribute attribute = new DataAttribute();
		attribute.Str = Dice.RollAttribute();
		attribute.Dex = Dice.RollAttribute();
		attribute.Con = Dice.RollAttribute();
		attribute.Int = Dice.RollAttribute();
		attribute.Wis = Dice.RollAttribute();
		attribute.Cha = Dice.RollAttribute();
		return attribute;
	}
	#endregion

	#region 扩展
	// 计算属性调整值（属性值-10）/2 向下取整
	public static int Modifier(int value) {
		return (int)System.Math.Floor((value - 10) / 2.0);
	}
	/// <summary> 添加属性 </summary>
	public static void Add(this DataAttribute a, DataAttribute b) {
		a.Str += b.Str; a.Dex += b.Dex;
		a.Con += b.Con; a.Int += b.Int;
		a.Wis += b.Wis; a.Cha += b.Cha;
	}
	/// <summary> 减少属性 </summary>
	public static void Sub(this DataAttribute a, DataAttribute b) {
		a.Str -= b.Str; a.Dex -= b.Dex;
		a.Con -= b.Con; a.Int -= b.Int;
		a.Wis -= b.Wis; a.Cha -= b.Cha;
	}
	/// <summary> 覆盖属性 </summary>
	public static void Cover(this DataAttribute a, DataAttribute b) {
		a.Str = b.Str; a.Dex = b.Dex;
		a.Con = b.Con; a.Int = b.Int;
		a.Wis = b.Wis; a.Cha = b.Cha;
	}
	#endregion
}
