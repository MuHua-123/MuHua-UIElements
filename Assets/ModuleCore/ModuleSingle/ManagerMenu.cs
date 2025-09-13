using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 菜单管理器
/// </summary>
public class ManagerMenu : ModuleSingle<ManagerMenu> {

	private List<DataMenuItem> datas = new List<DataMenuItem>();

	protected override void Awake() => NoReplace(false);

	private void Start() {
		// 初始化菜单项数据
		Add("建筑/伐木场", () => Debug.Log("伐木场"));
		Add("建筑/农场", () => Debug.Log("农场"));
		Add("建筑/矿场", () => Debug.Log("矿场"));
		Add("建筑/房屋/木屋", () => Debug.Log("木屋"));
		Add("建筑/房屋/豪宅", () => Debug.Log("豪宅"));

		Add("单位/步兵", () => Debug.Log("步兵"));
		Add("单位/骑兵", () => Debug.Log("骑兵"));

		Add("拆除", () => Debug.Log("拆除"));
	}

	public void Open() => UIShortcutMenu.I.Open(datas);

	public void Close() => UIShortcutMenu.I.Close();

	/// <summary> 添加菜单项(方法) </summary> 
	public void Add(string name, Action callback) {
		string[] names = name.Split('/');

		List<DataMenuItem> datas = this.datas;
		for (int i = 0; i < names.Length; i++) {
			string menu = names[i];
			DataMenuItem item = datas.Find(obj => obj.name == menu);
			if (item == null) {
				item = new DataMenuItem { name = menu };
				if (i == names.Length - 1) { item.callback = callback; }
				datas.Add(item);
			}
			datas = item.items;
		}
	}
	/// <summary> 移除菜单项 </summary>
	public void Remove(string name) {
		string[] names = name.Split('/');
		List<DataMenuItem> datas = this.datas;
		DataMenuItem parent = null;
		DataMenuItem target = null;

		for (int i = 0; i < names.Length; i++) {
			string menu = names[i];
			target = datas.Find(obj => obj.name == menu);
			if (target == null) return; // 未找到，直接返回
			if (i == names.Length - 1) {
				// 找到要移除的项
				datas.Remove(target);
				return;
			}
			parent = target;
			datas = target.items;
		}
	}
}
/// <summary>
/// 菜单项目
/// </summary>
public class DataMenuItem {
	/// <summary> 名称 </summary>
	public string name;
	/// <summary> 回调 </summary>
	public Action callback;
	/// <summary> 子菜单项 </summary>
	public List<DataMenuItem> items = new List<DataMenuItem>();
}