using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备
/// </summary>
public class Equipment : InventoryItem {
	/// <summary> 
	/// 装备类型：稀有度/装备类型/次级类型/种类
	/// <br/>稀有度：普通，精良，稀有，史诗，传奇，神话
	/// <br/>装备类型：武器，护甲，饰品
	/// <br/>次级类型：单手，副手，双手，上衣，头盔，手套，腰带，鞋子，项链，戒指，手镯
	/// <br/>装备种类：剑，锤，枪，弓，斧头，盾牌，法杖，魔杖
	///  </summary>
	public string type;
	/// <summary> 词缀列表 </summary>
	public List<EquipmentAffix> affixes = new List<EquipmentAffix>();
}