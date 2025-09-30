using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 装备面板
	/// </summary>
	public class UIEquipmentPanel : ModuleUIPanel {

		public WeaponConst weapon;
		public ArmorConst armor;
		public AccessoryConst accessory;

		public Label Name => Q<Label>("Name");
		public Label Type => Q<Label>("Type");

		public UIEquipmentPanel(VisualElement element) : base(element) {

		}

		public void Initial(EquipmentConst equipment) {
			element.EnableInClassList("ew-hide", equipment == null);
			if (equipment == null) { return; }
			Name.text = equipment.name;
			if (equipment is WeaponConst weapon) { InitialWeapon(weapon); }
			if (equipment is ArmorConst armor) { InitialArmor(armor); }
			if (equipment is AccessoryConst accessory) { InitialAccessory(accessory); }
		}

		public void InitialWeapon(WeaponConst weapon) {
			Type.text = $"Lv{weapon.level} {weapon.rarity} {weapon.type} {weapon.category}";
		}
		public void InitialArmor(ArmorConst armor) {
			Type.text = $"Lv{armor.level} {armor.rarity} {armor.type}";
		}
		public void InitialAccessory(AccessoryConst accessory) {
			Type.text = $"Lv{accessory.level} {accessory.rarity} {accessory.type}";
		}
	}
}
