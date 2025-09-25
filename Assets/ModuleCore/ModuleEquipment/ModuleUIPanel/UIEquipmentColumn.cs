using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI装备栏
/// </summary>
public class UIEquipmentColumn : ModuleUIPanel {

	public EquipmentColumn equipment;
	/// <summary> 插槽字典 </summary>
	public Dictionary<string, UIEquipmentSlot> dictionary = new Dictionary<string, UIEquipmentSlot>();

	public UIEquipmentColumn(VisualElement element) : base(element) {
		InitialSlot(1, EquipmentSlotType.主手);
		InitialSlot(2, EquipmentSlotType.副手);
		InitialSlot(3, EquipmentSlotType.上衣);
		InitialSlot(4, EquipmentSlotType.项链);

		InitialSlot(5, EquipmentSlotType.戒指1);
		InitialSlot(6, EquipmentSlotType.戒指2);
		InitialSlot(7, EquipmentSlotType.手镯1);
		InitialSlot(8, EquipmentSlotType.手镯2);

		InitialSlot(9, EquipmentSlotType.头盔);
		InitialSlot(10, EquipmentSlotType.手套);
		InitialSlot(11, EquipmentSlotType.腰带);
		InitialSlot(12, EquipmentSlotType.鞋子);
	}
	public void Dispose() {

	}

	public void Settings(EquipmentColumn equipment) {
		if (this.equipment != null) { Dispose(); }
		this.equipment = equipment;
		if (equipment == null) { return; }
		foreach (var item in dictionary) { Settings(item.Key, item.Value); }
	}
	public void Settings(string key, UIEquipmentSlot uISlot) {
		if (!equipment.ContainsKey(key)) { return; }
		EquipmentSlot slot = equipment[key];
		uISlot.Settings(slot);
	}

	private void InitialSlot(int index, EquipmentSlotType type) {
		VisualElement element = Q<VisualElement>($"EquipmentSlot{index}");
		UIEquipmentSlot uiSlot = new UIEquipmentSlot(element);
		dictionary.Add(type.ToString(), uiSlot);
	}
}
