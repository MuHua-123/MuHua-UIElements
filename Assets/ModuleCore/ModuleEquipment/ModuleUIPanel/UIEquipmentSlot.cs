using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI装备插槽
/// </summary>
public class UIEquipmentSlot : ModuleUIPanel, InventoryDrag {

	public EquipmentSlot value;

	public Label CountLabel => Q<Label>("Count");
	public VisualElement Image => Q<VisualElement>("Image");

	public int Count => 1;
	public InventoryItem Item => value.item;
	public VisualElement Anchor => element;

	public UIEquipmentSlot(VisualElement element) : base(element) {
		element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
		element.RegisterCallback<MouseOverEvent>(MouseOverEvent);
		element.RegisterCallback<MouseLeaveEvent>(MouseLeaveEvent);
		element.RegisterCallback<ClickEvent>(ClickEvent);
	}
	private void MouseDownEvent(MouseDownEvent evt) {
		UIPopupManager.I.inventoryDrag.Settings(this);
		Image.EnableInClassList("equipment-image-drag", true);
	}
	private void MouseOverEvent(MouseOverEvent evt) {
		if (!Verify()) { return; }
		UIPopupManager.I.inventoryDrag.EnterContainer(this);
	}
	private void MouseLeaveEvent(MouseLeaveEvent evt) {
		UIPopupManager.I.inventoryDrag.ExitContainer(this);
	}
	private void ClickEvent(ClickEvent evt) {
		// Debug.Log("sss");
	}

	public void Settings(EquipmentSlot value) {
		this.value = value;
		UpdatePreview();
	}
	public void Settings(InventoryItem item, int count) {
		value.Settings(item);
		UpdatePreview();
	}
	public void Cancel() {
		Image.EnableInClassList("equipment-image-drag", false);
	}

	private void UpdatePreview() {
		Sprite sprite = value == null || value.item == null ? null : value.item.sprite;
		Image.style.backgroundImage = new StyleBackground(sprite);
	}
	private bool Verify() {
		InventoryDrag container = UIPopupManager.I.inventoryDrag.container;
		if (container == null) { return false; }
		if (container.Item is Equipment equipment) { return value.verify.Invoke(equipment); }
		return false;
	}
}
