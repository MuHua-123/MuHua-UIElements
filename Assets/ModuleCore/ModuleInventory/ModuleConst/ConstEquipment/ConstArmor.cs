using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 护甲 - 物品常量
/// </summary>
[CreateAssetMenu(menuName = "MuHua/物品系统/护甲")]
public class ConstArmor : ConstEquipment {
	/// <summary> 护甲类型 </summary>
	public ArmorType type;

	public override DataItem To() {
		DataEquipment item = new DataEquipment();
		item.name = name;
		item.sprite = sprite;
		item.type = type.ToString();
		return item;
	}
}
/// <summary>
/// 护甲类型
/// </summary>
public enum ArmorType {
	上衣, 头盔, 手套, 腰带, 鞋子
}