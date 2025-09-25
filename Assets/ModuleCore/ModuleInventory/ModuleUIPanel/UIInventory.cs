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
/// <summary> 
/// UI库存插槽
/// </summary>
public class UIInventorySlot : ModuleUIItem<InventorySlot>, DragContainer {

	public Label CountLabel => Q<Label>("Count");
	public VisualElement Image => Q<VisualElement>("Image");

	public int Count => value.count;
	public DataItem Item => value.item;
	public VisualElement Anchor => element;

	public UIInventorySlot(InventorySlot value, VisualElement element) : base(value, element) {
		UpdatePreview();
		element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
		element.RegisterCallback<MouseOverEvent>(MouseOverEvent);
		element.RegisterCallback<MouseLeaveEvent>(MouseLeaveEvent);
		element.RegisterCallback<ClickEvent>(ClickEvent);
	}
	private void MouseDownEvent(MouseDownEvent evt) {
		UIPopupManager.I.dragItem.Settings(this);
		Image.EnableInClassList("inventory-image-drag", true);
	}
	private void MouseOverEvent(MouseOverEvent evt) {
		UIPopupManager.I.dragItem.EnterContainer(this);
	}
	private void MouseLeaveEvent(MouseLeaveEvent evt) {
		UIPopupManager.I.dragItem.ExitContainer(this);
	}
	private void ClickEvent(ClickEvent evt) {
		// Debug.Log("sss");
	}

	public void Settings(DataItem item, int count) {
		value.Settings(item, count);
		UpdatePreview();
	}
	public void Cancel() {
		Image.EnableInClassList("inventory-image-drag", false);
	}

	private void UpdatePreview() {
		CountLabel.text = value.count.ToString();
		CountLabel.EnableInClassList("inventory-count-hide", value.count <= 1);
		Image.style.backgroundImage = new StyleBackground(value.Sprite);
	}
}