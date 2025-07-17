using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 骰子
/// </summary>
public static class Dice {
	// 生成4d6弃最低的随机属性值
	public static int RollAttribute() {
		int[] rolls = { Random.Range(1, 7), Random.Range(1, 7), Random.Range(1, 7), Random.Range(1, 7) };
		Array.Sort(rolls);
		return rolls[1] + rolls[2] + rolls[3]; // 弃最低值
	}
}
