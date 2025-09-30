using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备品级
/// </summary>
public enum EquipmentRarity {
	/// <summary> 普通装备(白) 无词条 </summary>
	普通级,
	/// <summary> 精良装备(蓝) 词条数 1 - 10 完全随机 </summary>
	精良级,
	/// <summary> 稀有装备(紫) 词条数 4 - 8 保底4词条 </summary>
	稀有级,
	/// <summary> 传奇装备(黄) 词条数 4 - 8 固定4词条 </summary>
	传奇级,
	/// <summary> 传说装备(橙) 固定强力词条 </summary>
	传说级,
}
