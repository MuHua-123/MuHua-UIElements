using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI菜单管理器
/// </summary>
public class UIMenuManager : ModuleUISingle<UIMenuManager> {
	/// <summary> 菜单模板 </summary>
	public VisualTreeAsset menuTreeAsset;
	/// <summary> 项目模板 </summary>
	public VisualTreeAsset itemTreeAsset;

	private UIMenuPanel menu;

	public override VisualElement Element => root.Q<VisualElement>("Menu");

	protected override void Awake() => NoReplace(false);

	public void Open() {
		if (menu != null) { Close(); }
		Vector3 mousePosition = UITool.GetMousePosition(Element);
		menu = BuildMenu(mousePosition);
	}
	public void Close() {
		Element.Clear();
	}
	public void Settings() {

	}

	/// <summary> 创建菜单 </summary>
	public UIMenuPanel BuildMenu(Vector3 position) {
		VisualElement element = menuTreeAsset.Instantiate();
		Element.Add(element);
		element.transform.position = position;
		return new UIMenuPanel(element, itemTreeAsset);
	}
}
/// <summary> 
/// UI菜单面板
/// </summary>
public class UIMenuPanel : ModuleUIPanel {

	public ModuleUIItems<UIItem, DataMenuItem> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIMenuPanel(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
		items = new ModuleUIItems<UIItem, DataMenuItem>(Container, templateAsset,
		(data, element) => new UIItem(data, element));
	}
	public void Release() => items.Release();

	/// <summary> UI项目 </summary>
	public class UIItem : ModuleUIItem<DataMenuItem> {
		public UIItem(DataMenuItem value, VisualElement element) : base(value, element) {

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
}