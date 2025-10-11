using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 快捷菜单
/// </summary>
public class ShortcutMenu : Module<ShortcutMenu> {
	/// <summary> 数据列表 </summary>
	public List<MenuItem> menuItems = new List<MenuItem>();

	/// <summary> 打开菜单 </summary>
	public void Open() => UIShortcutMenu.I?.Open();

	/// <summary> 关闭菜单 </summary>
	public void Close() => UIShortcutMenu.I?.Close();

	/// <summary> 添加菜单项(1级菜单/2级菜单/3级菜单) </summary> 
	public void Add(string name, Action callback) {
		string[] names = name.Split('/');
		MenuItem item = Find(names[0], menuItems, true);
		for (int i = 1; i < names.Length; i++) {
			item = Find(names[i], item.menuItems, true);
		}
		item.callback = callback;
	}
	/// <summary> 移除菜单项(???/???/子级菜单) </summary>
	public void Remove(string name) {
		string[] names = name.Split('/');
		List<MenuItem> menuItems = this.menuItems;
		MenuItem item = Find(names[0], menuItems, false);
		for (int i = 1; i < names.Length; i++) {
			if (item == null) return;
			menuItems = item.menuItems;
			item = Find(names[i], menuItems, false);
		}
		menuItems.Remove(item);
	}

	/// <summary> 子项目查找 </summary> 
	private MenuItem Find(string menu, List<MenuItem> menuItems, bool isCreate) {
		MenuItem item = menuItems.Find(obj => obj.name == menu);
		if (item != null || !isCreate) { return item; }
		item = new MenuItem { name = menu };
		menuItems.Add(item);
		return item;
	}
}
