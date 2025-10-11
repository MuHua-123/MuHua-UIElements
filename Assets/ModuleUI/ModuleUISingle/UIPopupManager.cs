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

	public UIInventoryDrag inventoryDrag;

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement DragItem => Q<VisualElement>("DragItem");

	protected override void Awake() {
		NoReplace(false);
		inventoryDrag = new UIInventoryDrag(DragItem, root);
	}
}
