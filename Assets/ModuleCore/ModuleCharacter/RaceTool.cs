using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 种族 - 工具
/// </summary>
public static class RaceTool {

	#region 创建
	// 混血人类，木精灵，丘陵矮人(Hill Dwarf)，龙裔，半精灵
	/// <summary> 无种族 </summary>
	public static DataRace None() {
		return new DataRace { name = "未知" };
	}
	/// <summary> 随机职业 </summary>
	public static DataRace Random() {
		int index = Dice.Roll(5);
		if (index == 1) { return Human(); }
		if (index == 2) { return Elven(); }
		if (index == 3) { return Dwarf(); }
		if (index == 4) { return Orc(); }
		if (index == 5) { return Halfling(); }
		return None();
	}
	/// <summary> 人类(Human) 全属性+1 </summary>
	public static DataRace Human() {
		return new DataRace { name = "人类", Str = 1, Dex = 1, Con = 1, Int = 1, Wis = 1, Cha = 1 };
	}
	/// <summary> 精灵(Elven) 敏捷+2 智力+2 感知+1 </summary>
	public static DataRace Elven() {
		return new DataRace { name = "精灵", Dex = 2, Int = 2, Wis = 1 };
	}
	/// <summary> 矮人(Dwarf) 力量+2 体质+2 感知+1 </summary>
	public static DataRace Dwarf() {
		return new DataRace { name = "矮人", Str = 2, Con = 2, Wis = 1 };
	}
	/// <summary> 半兽人(Orc) 力量+3 体质+2 </summary>
	public static DataRace Orc() {
		return new DataRace { name = "半兽人", Str = 3, Con = 2 };
	}
	/// <summary> 半身人(Halfling) 敏捷+2 魅力+2 体质+1 </summary>
	public static DataRace Halfling() {
		return new DataRace { name = "半身人", Dex = 2, Cha = 2, Con = 1 };
	}
	#endregion
}
