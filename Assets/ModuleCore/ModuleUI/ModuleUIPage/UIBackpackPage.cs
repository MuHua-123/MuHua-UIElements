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

	private UIEquipment equipment;
	private UIInventory inventory;

	public override VisualElement Element => root.Q<VisualElement>("BackpackPage");

	public VisualElement Equipment => Q<VisualElement>("Equipment");
	public VisualElement Inventory => Q<VisualElement>("Inventory");

	protected void Awake() {
		equipment = new UIEquipment(Equipment);
		inventory = new UIInventory(Inventory, inventorySlot);

		ModuleUI.OnJumpPage += ModuleUI_OnJumpPage;
	}

	private void ModuleUI_OnJumpPage(Page page) {
		Element.EnableInClassList("document-page-hide", page != Page.Backpack);
		if (page != Page.Backpack) { return; }
		equipment.Settings(SingleManager.I.equipment);
		inventory.Settings(SingleManager.I.inventory);
	}
}
