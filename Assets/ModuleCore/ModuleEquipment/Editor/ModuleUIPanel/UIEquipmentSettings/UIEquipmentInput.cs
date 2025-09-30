using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 装备输入
	/// </summary>
	public class UIEquipmentInput : ModuleUIPanel {

		public WeaponConst weapon;
		public EquipmentConst equipment;

		public TextField Name => Q<TextField>("Name");
		public IntegerField Level => Q<IntegerField>("Level");
		public ObjectField Sprite => Q<ObjectField>("Sprite");
		public EnumField Rarity => Q<EnumField>("Rarity");
		public EnumField WeaponCategory => Q<EnumField>("WeaponCategory");

		public UIEquipmentInput(VisualElement element) : base(element) {
			Name.RegisterCallback<FocusOutEvent>(evt => ModifyName());
			Level.RegisterCallback<FocusOutEvent>(evt => ModifyLevel());
			Sprite.RegisterCallback<FocusOutEvent>(evt => ModifySprite());
			Rarity.RegisterValueChangedCallback(evt => ModifyRarity((EquipmentRarity)evt.newValue));
			WeaponCategory.RegisterValueChangedCallback(evt => ModifyWeaponType((WeaponCategory)evt.newValue));
		}

		public void Initial(EquipmentConst equipment) {
			this.equipment = equipment;
			if (equipment == null) { return; }
			Name.SetValueWithoutNotify(equipment.name);
			Level.SetValueWithoutNotify(equipment.level);
			Sprite.SetValueWithoutNotify(equipment.sprite);
			Rarity.Init(equipment.rarity);
			weapon = equipment as WeaponConst;
			WeaponCategory.EnableInClassList("ew-hide", weapon == null);
			if (weapon == null) { return; }
			WeaponCategory.Init(weapon.category);
		}

		/// <summary> 修改名字 </summary>
		private void ModifyName() {
			if (Name.value == null || Name.value == "") {
				Name.SetValueWithoutNotify(equipment.name);
				return;
			}
			string oldPath = AssetDatabase.GetAssetPath(equipment);
			AssetDatabase.RenameAsset(oldPath, Name.value);
			equipment.name = Name.value;
			Dirty();
		}
		/// <summary> 修改类型 </summary>
		private void ModifyLevel() { equipment.level = Level.value; Dirty(); }
		/// <summary> 修改预览图 </summary>
		private void ModifySprite() { equipment.sprite = Sprite.value as Sprite; Dirty(); }
		/// <summary> 修改布尔值 </summary>
		private void ModifyRarity(EquipmentRarity rarity) { equipment.rarity = rarity; Dirty(); }
		/// <summary> 修改武器类型 </summary>
		private void ModifyWeaponType(WeaponCategory category) { weapon.category = category; Dirty(); }

		/// <summary> 保存资源 </summary>
		private void Dirty() {
			EditorUtility.SetDirty(equipment);
			AssetDatabase.SaveAssets();
		}
	}
}
