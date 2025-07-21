using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 骰子
/// </summary>
public static class Dice {
	/// <summary> 掷一次骰子 </summary>
	public static int Roll(int value) {
		return Random.Range(1, value + 1);
	}
	/// <summary> d20 + 调整值 + 加值 </summary>
	public static int Roll20(int modifier = 0, int addValue = 0, DiceGrade grade = DiceGrade.平势) {
		int d1 = Roll(20); int d2 = Roll(20);
		if (grade == DiceGrade.优势) { d1 = d1 >= d2 ? d1 : d2; }
		if (grade == DiceGrade.平势) { d1 = d1 <= d2 ? d1 : d2; }
		return d1 + modifier + addValue;
	}
	/// <summary> Attribute规则：投4次d6，去掉最低值 </summary>
	public static int RollAttribute() {
		int[] rolls = { Roll(6), Roll(6), Roll(6), Roll(6) };
		Array.Sort(rolls);
		return rolls[1] + rolls[2] + rolls[3]; // 弃最低值
	}
}
public enum DiceGrade {
	平势 = 0,
	优势 = 1,
	劣势 = 2
}