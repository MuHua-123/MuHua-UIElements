using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI弹出管理器
/// </summary>
public class UIPopupManager : ModuleUISingle<UIPopupManager> {
	/// <summary> 菜单模板 </summary>
	public VisualTreeAsset menuTreeAsset;
	/// <summary> 项目模板 </summary>
	public VisualTreeAsset itemTreeAsset;

	public UIDragItem dragItem;
	public UIShortcutMenu shortcutMenu;

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement DragItem => Q<VisualElement>("DragItem");
	public VisualElement ShortcutMenu => Q<VisualElement>("ShortcutMenu");

	protected override void Awake() {
		NoReplace(false);
		dragItem = new UIDragItem(DragItem, root);
		shortcutMenu = new UIShortcutMenu(ShortcutMenu, menuTreeAsset, itemTreeAsset);
	}
}
