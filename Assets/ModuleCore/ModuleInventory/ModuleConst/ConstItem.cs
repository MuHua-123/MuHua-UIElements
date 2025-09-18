using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品 - 常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/物品")]
public class ConstItem : ScriptableObject {
	/// <summary> 图片 </summary>
	public Sprite sprite;

	/// <summary> 数据转换 </summary>
	public virtual DataItem To() {
		DataItem item = new DataItem();
		item.name = name;
		item.sprite = sprite;
		return item;
	}
}
