using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI装备栏
/// </summary>
public class UIEquipment : ModuleUIPanel {

	public VisualElement EquipmentSlot1 => Q<VisualElement>("EquipmentSlot1");// 主手
	public VisualElement EquipmentSlot2 => Q<VisualElement>("EquipmentSlot2");// 副手
	public VisualElement EquipmentSlot3 => Q<VisualElement>("EquipmentSlot3");// 衣服
	public VisualElement EquipmentSlot4 => Q<VisualElement>("EquipmentSlot4");// 项链
	public VisualElement EquipmentSlot5 => Q<VisualElement>("EquipmentSlot5");// 戒指1
	public VisualElement EquipmentSlot6 => Q<VisualElement>("EquipmentSlot6");// 戒指2
	public VisualElement EquipmentSlot7 => Q<VisualElement>("EquipmentSlot7");// 手镯1
	public VisualElement EquipmentSlot8 => Q<VisualElement>("EquipmentSlot8");// 手镯2
	public VisualElement EquipmentSlot9 => Q<VisualElement>("EquipmentSlot9");// 头盔
	public VisualElement EquipmentSlot10 => Q<VisualElement>("EquipmentSlot10");// 手套
	public VisualElement EquipmentSlot11 => Q<VisualElement>("EquipmentSlot11");// 腰带
	public VisualElement EquipmentSlot12 => Q<VisualElement>("EquipmentSlot12");// 鞋子

	public UIEquipment(VisualElement element) : base(element) {

	}
}
/// <summary>
/// UI装备插槽
/// </summary>
public class UIEquipmentSlot : ModuleUIPanel, DragContainer {

	public EquipmentSlot value;

	public Label CountLabel => Q<Label>("Count");
	public VisualElement Image => Q<VisualElement>("Image");

	public int Count => 1;
	public DataItem Item => value.item;
	public VisualElement Anchor => element;

	public UIEquipmentSlot(VisualElement element) : base(element) {
		element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
		element.RegisterCallback<MouseOverEvent>(MouseOverEvent);
		element.RegisterCallback<MouseLeaveEvent>(MouseLeaveEvent);
		element.RegisterCallback<ClickEvent>(ClickEvent);
	}
	private void MouseDownEvent(MouseDownEvent evt) {
		UIPopupManager.I.dragItem.Settings(this);
		Image.EnableInClassList("equipment-image-drag", true);
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

	public void Settings(EquipmentSlot value) {
		this.value = value;
		UpdatePreview();
	}
	public void Cancel() {
		Image.EnableInClassList("equipment-image-drag", false);
	}
	public void Exchange(DragContainer container) {
		int count = container.Count;
		DataEquipment item = container.Item as DataEquipment;

		if (container is UIInventory.UIItem inventoryItem) {
			inventoryItem.value.Settings(value.item, 1);
			inventoryItem.UpdatePreview();
		}

		value.item = item;
		UpdatePreview();
	}

	private void UpdatePreview() {
		Sprite sprite = value == null || value.item == null ? null : value.item.sprite;
		Image.style.backgroundImage = new StyleBackground(sprite);
	}
}