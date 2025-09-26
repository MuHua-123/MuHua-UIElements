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
	private UIEquipmentColumn equipment;

	public override VisualElement Element => root.Q<VisualElement>("BackpackPage");

	public VisualElement Inventory => Q<VisualElement>("Inventory");
	public VisualElement EquipmentColumn => Q<VisualElement>("EquipmentColumn");

	protected void Awake() {
		equipment = new UIEquipmentColumn(EquipmentColumn);
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
