using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 背包页面
/// </summary>
public class UIBackpackPage : ModuleUIPage {
	public VisualTreeAsset inventorySlot;

	private UIInventory inventory;

	public override VisualElement Element => root.Q<VisualElement>("BackpackPage");

	public VisualElement Inventory => Q<VisualElement>("Inventory");

	protected void Awake() {
		inventory = new UIInventory(Inventory, inventorySlot);

		ModuleUI.OnJumpPage += ModuleUI_OnJumpPage;
	}

	private void ModuleUI_OnJumpPage(Page page) {
		Element.EnableInClassList("document-page-hide", page != Page.Backpack);
		if (page != Page.Backpack) { return; }
		inventory.Settings(SingleManager.I.inventory);
	}
}
