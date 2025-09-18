using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;
using System;

/// <summary>
/// UI库存
/// </summary>
public class UIInventory : ModuleUIPanel {

	public Inventory inventory;
	public ModuleUIItems<UIItem, InventorySlot> items;

	public VisualElement Container => Q<VisualElement>("Container");

	public UIInventory(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
		items = new ModuleUIItems<UIItem, InventorySlot>(Container, templateAsset,
		(data, element) => new UIItem(data, element));
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

	/// <summary> UI项目 </summary>
	public class UIItem : ModuleUIItem<InventorySlot> {

		public Label Count => Q<Label>("Count");
		public VisualElement Image => Q<VisualElement>("Image");

		public UIItem(InventorySlot value, VisualElement element) : base(value, element) {
			Count.text = value.count.ToString();
			Count.EnableInClassList("inventory-count-hide", value.count <= 1);
			Image.style.backgroundImage = new StyleBackground(value.Sprite);
			element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
			element.RegisterCallback<ClickEvent>(ClickEvent);
		}
		private void MouseDownEvent(MouseDownEvent evt) {
			UIPopupManager.I.dragItem.Open(value.item, value.count);
		}
		private void ClickEvent(ClickEvent evt) {
			Debug.Log("sss");
		}
	}
}
