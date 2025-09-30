using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 装备设置
	/// </summary>
	public class UIEquipmentSettings : ModuleUIPanel {

		public EquipmentConst equipment;
		public UIEquipmentInput equipmentInput;

		public VisualElement EquipmentInput => Q<VisualElement>("EquipmentInput");

		public UIEquipmentSettings(VisualElement element) : base(element) {
			equipmentInput = new UIEquipmentInput(EquipmentInput);
		}

		public void Initial(EquipmentConst equipment) {
			this.equipment = equipment;
			element.EnableInClassList("ew-hide", equipment == null);
			equipmentInput.Initial(equipment);
		}
	}

}
