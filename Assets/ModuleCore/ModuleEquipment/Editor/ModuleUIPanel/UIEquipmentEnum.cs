using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 装备枚举
	/// </summary>
	public class UIEquipmentEnum<T> : ModuleUIPanel where T : Enum {

		public Action<T> callback;
		public ModuleUIItems<UIItem, T> items;

		public UIEquipmentEnum(VisualElement element, VisualTreeAsset templateAsset, Action<T> callback) : base(element) {
			this.callback = callback;
			items = new ModuleUIItems<UIItem, T>(element, templateAsset,
			(data, element) => new UIItem(data, element, this));
		}
		/// <summary> 释放资源 </summary>
		public void Release() => items.Release();

		/// <summary> 初始化 </summary>
		public void Initial() {
			items.Create(EnumToList());
			items.SelectFirst();
		}
		/// <summary> 设置值 </summary>
		public void Settings(T value) => callback?.Invoke(value);

		private List<T> EnumToList() {
			var list = new List<T>();
			foreach (var name in Enum.GetValues(typeof(T))) { list.Add((T)name); }
			return list;
		}

		/// <summary> UI项 </summary>
		public class UIItem : ModuleUIItem<T> {
			public readonly UIEquipmentEnum<T> parent;

			public Button Button => Q<Button>("Button");

			public UIItem(T value, VisualElement element, UIEquipmentEnum<T> parent) : base(value, element) {
				this.parent = parent;
				Button.text = value.ToString();
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