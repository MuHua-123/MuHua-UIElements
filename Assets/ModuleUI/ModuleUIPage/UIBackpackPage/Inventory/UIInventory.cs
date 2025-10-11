using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI库存
/// </summary>
public class UIInventory : ModuleUIPanel {

	public Inventory inventory;
	public ModuleUIItems<UIInventorySlot, InventorySlot> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIInventory(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
		items = new ModuleUIItems<UIInventorySlot, InventorySlot>(Container, templateAsset,
		(data, element) => new UIInventorySlot(data, element));
	}
	public void Dispose() {
		items.Release();
		if (inventory == null) { return; }
		inventory.OnChange -= Inventory_OnChange;
	}

	public void Settings(Inventory inventory) {
		if (this.inventory != null) { Dispose(); }
		this.inventory = inventory;
		if (inventory == null) { return; }
		items.Create(inventory.slots);
		inventory.OnChange += Inventory_OnChange;
	}

	private void Inventory_OnChange(Inventory inventory) {
		items.Create(inventory.slots);
	}
}
