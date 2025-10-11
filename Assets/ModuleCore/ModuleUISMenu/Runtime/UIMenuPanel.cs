using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary> 
/// UI快捷菜单面板
/// </summary>
public class UIMenuPanel : ModuleUIPanel, UIControl {

	public UIMenuPanel menuPanel;
	public VisualElement submenu;
	public ModuleUIItems<UIItem, ShortcutMenuItem> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIMenuPanel(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
		items = new ModuleUIItems<UIItem, ShortcutMenuItem>(Container, templateAsset,
		(data, element) => new UIItem(data, element, this));
	}
	public void Update() {
		// throw new NotImplementedException();
	}
	public void Dispose() {
		items.Release();
	}

	public void Settings(Vector3 position, List<ShortcutMenuItem> datas) {
		items.Create(datas);
		element.transform.position = position;
	}

	/// <summary> 打开子菜单 </summary>
	public void Open(VisualElement submenu, List<ShortcutMenuItem> datas) {
		if (this.submenu == submenu) { return; }
		// 更新子菜单
		this.submenu = submenu;
		if (menuPanel == null) { menuPanel = UIShortcutMenu.I.Create(); }
		float x = submenu.worldBound.position.x + submenu.resolvedStyle.width;
		float y = submenu.worldBound.position.y - 4;
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
	public class UIItem : ModuleUIItem<ShortcutMenuItem> {
		public readonly UIMenuPanel parent;

		public Label Name => element.Q<Label>("Name");
		public VisualElement Arrow => element.Q<VisualElement>("Arrow");

		public UIItem(ShortcutMenuItem value, VisualElement element, UIMenuPanel parent) : base(value, element) {
			this.parent = parent;
			Name.text = value.name;
			Arrow.EnableInClassList("menu-arrow-hide", value.menuItems == null || value.menuItems.Count == 0);
			element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
			element.RegisterCallback<MouseMoveEvent>(MouseMoveEvent);
		}
		private void MouseDownEvent(MouseDownEvent evt) {
			value.callback?.Invoke();
			UIShortcutMenu.I.Close();
		}
		private void MouseMoveEvent(MouseMoveEvent evt) {
			parent.Open(element, value.menuItems);
		}
	}
}