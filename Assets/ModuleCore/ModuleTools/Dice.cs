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
	/// <summary> Attribute规则：投4次d6，去掉最低值 </summary>
	public static int RollAttribute() {
		int[] rolls = { Roll(6), Roll(6), Roll(6), Roll(6) };
		Array.Sort(rolls);
		return rolls[1] + rolls[2] + rolls[3]; // 弃最低值
	}
}
