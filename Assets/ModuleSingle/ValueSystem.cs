using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 数值系统
/// </summary>
public class ValueSystem : Module<ValueSystem> {
	/// <summary> 对战数值 </summary>
	public ValueContainer battle;

	/// <summary> 初始化 </summary>
	public void Initial() {
		battle = new ValueContainer();
		battle.AddInstance(ValueType.Integer, "生命");
		battle.AddInstance(ValueType.Integer, "金币");
		battle.AddInstance(ValueType.Integer, "水晶");

		Debug.Log(battle["生命"].value);

		ValueModifier modifier = new ValueModifier { Fixed = 100 };
		battle["生命"].AddModifier(modifier, true);

		Debug.Log(battle["生命"].value);
	}
}
