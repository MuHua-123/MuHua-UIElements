using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI装备栏
/// </summary>
public class UIEquipment : ModuleUIPanel {

	public Equipment equipment;
	/// <summary> 插槽字典 </summary>
	public Dictionary<string, UIEquipmentSlot> dictionary = new Dictionary<string, UIEquipmentSlot>();

	public UIEquipment(VisualElement element) : base(element) {
		InitialSlot(1, SlotType.主手);
		InitialSlot(2, SlotType.副手);
		InitialSlot(3, SlotType.上衣);
		InitialSlot(4, SlotType.项链);

		InitialSlot(4, SlotType.戒指1);
		InitialSlot(4, SlotType.戒指2);
		InitialSlot(4, SlotType.手镯1);
		InitialSlot(4, SlotType.手镯2);

		InitialSlot(4, SlotType.头盔);
		InitialSlot(4, SlotType.手套);
		InitialSlot(4, SlotType.腰带);
		InitialSlot(4, SlotType.鞋子);
	}
	public void Dispose() {

	}

	public void Settings(Equipment equipment) {
		if (this.equipment != null) { Dispose(); }
		this.equipment = equipment;
		if (equipment == null) { return; }
		foreach (var item in dictionary) {
			if (!equipment.ContainsKey(item.Key)) { continue; }
			EquipmentSlot slot = equipment[item.Key];
			item.Value.Settings(slot);
		}
	}

	private void InitialSlot(int index, SlotType type) {
		VisualElement element = Q<VisualElement>($"EquipmentSlot{index}");
		UIEquipmentSlot uiSlot = new UIEquipmentSlot(element);
		dictionary.Add(type.ToString(), uiSlot);
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
		if (!Verify()) { return; }
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
	public void Settings(DataItem item, int count) {
		value.Settings(item, count);
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
		DragContainer container = UIPopupManager.I.dragItem.container;
		if (container == null) { return false; }
		if (container.Item is DataEquipment equipment) { return value.Verify(equipment); }
		return false;
	}
}