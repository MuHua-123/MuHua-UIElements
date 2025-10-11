using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 菜单项目
/// </summary>
public class MenuItem {
	/// <summary> 名称 </summary>
	public string name;
	/// <summary> 回调 </summary>
	public Action callback;
	/// <summary> 子菜单项 </summary>
	public List<MenuItem> menuItems = new List<MenuItem>();
}
