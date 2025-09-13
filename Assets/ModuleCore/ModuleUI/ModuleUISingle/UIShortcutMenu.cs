using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI快捷菜单
/// </summary>
public class UIShortcutMenu : ModuleUISingle<UIShortcutMenu> {
	/// <summary> 菜单模板 </summary>
	public VisualTreeAsset menuTreeAsset;
	/// <summary> 项目模板 </summary>
	public VisualTreeAsset itemTreeAsset;
	/// <summary> 控件列表 </summary>
	public static List<UIControl> controls = new List<UIControl>();

	public override VisualElement Element => root.Q<VisualElement>("ShortcutMenu");

	protected override void Awake() => NoReplace(false);

	private void Update() => controls.ForEach(control => control.Update());

	private void OnDestroy() => controls.ForEach(control => control.Dispose());

	/// <summary> 打开菜单 </summary>
	public void Open(List<DataMenuItem> datas) {
		Close();
		Vector3 position = UITool.GetMousePosition(Element);
		UIMenuPanel menuPanel = Create();
		menuPanel.Settings(position, datas);
	}
	/// <summary> 关闭菜单 </summary>
	public void Close() {
		controls.ForEach(control => control.Dispose());
		controls.Clear();
		Element.Clear();
	}
	/// <summary> 创建子菜单 </summary>
	public UIMenuPanel Create() {
		// 创建菜单元素
		VisualElement element = menuTreeAsset.Instantiate();
		element.EnableInClassList("menu", true);
		Element.Add(element);
		UIMenuPanel menuPanel = new UIMenuPanel(element, itemTreeAsset);
		AddControl(menuPanel);
		return menuPanel;
	}

	/// <summary> 添加控件 </summary>
	public static void AddControl(UIControl control) => controls.Add(control);
	/// <summary> 移除控件 </summary>
	public static void RemoveControl(UIControl control) => controls.Remove(control);
}
/// <summary> 
/// UI快捷菜单面板
/// </summary>
public class UIMenuPanel : ModuleUIPanel, UIControl {

	public UIMenuPanel menuPanel;
	public VisualElement submenu;
	public ModuleUIItems<UIItem, DataMenuItem> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIMenuPanel(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
		items = new ModuleUIItems<UIItem, DataMenuItem>(Container, templateAsset,
		(data, element) => new UIItem(data, element, this));
	}
	public void Update() {
		// throw new NotImplementedException();
	}
	public void Dispose() {
		items.Release();
	}

	public void Settings(Vector3 position, List<DataMenuItem> datas) {
		items.Create(datas);
		element.transform.position = position;
	}

	/// <summary> 打开子菜单 </summary>
	public void Open(VisualElement submenu, List<DataMenuItem> datas) {
		if (this.submenu == submenu) { return; }
		// 更新子菜单
		this.submenu = submenu;
		if (menuPanel == null) { menuPanel = UIShortcutMenu.I.Create(); }
		float x = submenu.worldBound.position.x + submenu.resolvedStyle.width;
		float y = submenu.worldBound.position.y - 5;
		Vector3 position = new Vector3(x, y, 0);
		menuPanel.Settings(position, datas);
		bool isEnable = datas != null && datas.Count > 0;
		menuPanel.OpenSubmenu(isEnable);
	}
	public void OpenSubmenu(bool open) {
		element.EnableInClassList("menu-hide", !open);
		menuPanel?.OpenSubmenu(false);
	}

	/// <summary> UI项目 </summary>
	public class UIItem : ModuleUIItem<DataMenuItem> {
		public readonly UIMenuPanel parent;

		public Label Name => element.Q<Label>("Name");
		public VisualElement Arrow => element.Q<VisualElement>("Arrow");

		public UIItem(DataMenuItem value, VisualElement element, UIMenuPanel parent) : base(value, element) {
			this.parent = parent;
			Name.text = value.name;
			Arrow.EnableInClassList("menu-arrow-hide", value.items == null || value.items.Count == 0);
			element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
			element.RegisterCallback<MouseMoveEvent>(MouseMoveEvent);
		}
		private void MouseDownEvent(MouseDownEvent evt) {
			value.callback?.Invoke();
			UIShortcutMenu.I.Close();
		}
		private void MouseMoveEvent(MouseMoveEvent evt) {
			parent.Open(element, value.items);
		}
	}
}
