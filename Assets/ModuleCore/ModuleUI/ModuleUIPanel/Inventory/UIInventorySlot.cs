using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary> 
/// UI库存插槽
/// </summary>
public class UIInventorySlot : ModuleUIItem<InventorySlot>, InventoryDrag {

	public Label CountLabel => Q<Label>("Count");
	public VisualElement Image => Q<VisualElement>("Image");

	public int Count => value.count;
	public InventoryItem Item => value.item;
	public VisualElement Anchor => element;

	public UIInventorySlot(InventorySlot value, VisualElement element) : base(value, element) {
		UpdatePreview();
		element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
		element.RegisterCallback<MouseOverEvent>(MouseOverEvent);
		element.RegisterCallback<MouseLeaveEvent>(MouseLeaveEvent);
		element.RegisterCallback<ClickEvent>(ClickEvent);
	}
	private void MouseDownEvent(MouseDownEvent evt) {
		UIPopupManager.I.inventoryDrag.Settings(this);
		Image.EnableInClassList("inventory-image-drag", true);
	}
	private void MouseOverEvent(MouseOverEvent evt) {
		UIPopupManager.I.inventoryDrag.EnterContainer(this);
	}
	private void MouseLeaveEvent(MouseLeaveEvent evt) {
		UIPopupManager.I.inventoryDrag.ExitContainer(this);
	}
	private void ClickEvent(ClickEvent evt) {
		// Debug.Log("sss");
	}

	public void Settings(InventoryItem item, int count) {
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
