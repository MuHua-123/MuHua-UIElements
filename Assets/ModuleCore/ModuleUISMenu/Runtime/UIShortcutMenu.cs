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
	public VisualTreeAsset MenuPanel;
	/// <summary> 项目模板 </summary>
	public VisualTreeAsset MenuTemplate;

	/// <summary> 控件列表 </summary>
	public static List<UIControl> controls = new List<UIControl>();

	public override VisualElement Element => root.Q<VisualElement>("ShortcutMenu");

	protected override void Awake() => NoReplace(false);

	private void Update() => controls.ForEach(control => control.Update());

	private void OnDestroy() => controls.ForEach(control => control.Dispose());

	/// <summary> 打开菜单 </summary>
	public void Open() {
		Close();
		Vector3 position = UITool.GetMousePosition(Element);
		UIMenuPanel menuPanel = Create();
		menuPanel.Settings(position, ShortcutMenu.I.menuItems);
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
		VisualElement element = MenuPanel.Instantiate();
		element.EnableInClassList("menu", true);
		Element.Add(element);
		UIMenuPanel menuPanel = new UIMenuPanel(element, MenuTemplate);
		controls.Add(menuPanel);
		return menuPanel;
	}
}
