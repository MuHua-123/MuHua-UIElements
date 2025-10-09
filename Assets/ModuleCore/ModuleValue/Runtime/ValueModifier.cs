using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数值修改器
/// </summary>
public class ValueModifier {
	/// <summary> 固定值 </summary>
	public float Fixed;
	/// <summary> 附加值 </summary>
	public float Added;

	/// <summary> 固定值 </summary>
	public virtual float FixedValue => Fixed;
	/// <summary> 附加值% </summary>
	public virtual float AddedValue => Added;
}
