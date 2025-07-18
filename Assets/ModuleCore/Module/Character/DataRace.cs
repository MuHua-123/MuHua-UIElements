using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 种族 - 数据
/// </summary>
public class DataRace : DataAttribute {
	/// <summary> 种族名称 </summary>
	public string name;
	/// <summary> 基础生命点 </summary>
	public int hitPoint = 10;

	/// <summary> 生命点：每级生命点总和 </summary>
	public int HitPoint() {
		return hitPoint;
	}

	/// <summary> 无种族 </summary>
	public static DataRace None() {
		// 初始生命值10点
		return new DataRace() { name = "无" };
	}
}
