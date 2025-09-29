using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 装备 - UI列表
	/// </summary>
	public class UIEquipmentList : ModuleUIPanel, UIControl {

		public Action<EquipmentConst> callback;
		public UIScrollViewListV<UIItem, EquipmentConst> items;

		public VisualElement ScrollView => Q<VisualElement>("ScrollView");

		public UIEquipmentList(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset, Action<EquipmentConst> callback) : base(element) {
			this.callback = callback;
			items = new UIScrollViewListV<UIItem, EquipmentConst>(ScrollView, canvas, templateAsset,
			(data, element) => new UIItem(data, element, this));
		}

		public void Update() => items.Update();

		public void Dispose() => items.Dispose();

		/// <summary> 初始化 </summary>
		public void Initial(List<EquipmentConst> equipments) {
			items.Create(equipments);
			items.SelectFirst();
		}

		/// <summary> 设置值 </summary>
		public void Settings(EquipmentConst value) => callback?.Invoke(value);

		/// <summary> UI项 </summary>
		public class UIItem : ModuleUIItem<EquipmentConst> {
			public readonly UIEquipmentList parent;

			public Button Button => Q<Button>("Button");

			public UIItem(EquipmentConst value, VisualElement element, UIEquipmentList parent) : base(value, element) {
				this.parent = parent;
				Button.text = value.name;
				Button.clicked += Select;
			}
			public override void DefaultState() {
				Button.EnableInClassList("button-s", false);
			}
			public override void SelectState() {
				parent.Settings(value);
				Button.EnableInClassList("button-s", true);
			}
		}
	}
}