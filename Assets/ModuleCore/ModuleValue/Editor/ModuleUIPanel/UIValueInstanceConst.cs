using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 数值实例 - UI面板
	/// </summary>
	public class UIValueInstanceConst : ModuleUIPanel {

		private ValueInstanceConst value;

		public TextField Name => Q<TextField>("Name");
		public EnumField Type => Q<EnumField>("Type");
		public FloatField Min => Q<FloatField>("Min");
		public FloatField Max => Q<FloatField>("Max");
		public FloatField Base => Q<FloatField>("Base");
		public Toggle Boolean => Q<Toggle>("Boolean");

		public UIValueInstanceConst(ValueInstanceConst value, VisualElement element) : base(element) {
			this.value = value;

			Name.SetValueWithoutNotify(value.name);
			Type.Init(value.type);
			Min.SetValueWithoutNotify(value.minValue);
			Max.SetValueWithoutNotify(value.maxValue);
			Base.SetValueWithoutNotify(value.baseValue);
			Boolean.value = value.baseValue == value.maxValue;

			VisualElement temp = value.type == ValueType.Boolean ? Base : Boolean;
			temp.style.display = DisplayStyle.None;

			Name.RegisterCallback<FocusOutEvent>(evt => ModifyName());
			Type.RegisterValueChangedCallback(evt => ModifyType((ValueType)evt.newValue));
			Min.RegisterCallback<FocusOutEvent>(evt => ModifyMin());
			Max.RegisterCallback<FocusOutEvent>(evt => ModifyMax());
			Base.RegisterCallback<FocusOutEvent>(evt => ModifyValue());
			Boolean.RegisterValueChangedCallback(evt => ModifyBoolean());
		}

		/// <summary> 修改名字 </summary>
		private void ModifyName() { value.name = Name.value; Dirty(); }
		/// <summary> 修改类型 </summary>
		private void ModifyType(ValueType type) {
			if (type == ValueType.Float) { value.InitialFloat(); }
			if (type == ValueType.Integer) { value.InitialInteger(); }
			if (type == ValueType.Boolean) { value.InitialBoolean(); }
			if (type == ValueType.Percentage) { value.InitialPercentage(); }

			VisualElement temp = value.type == ValueType.Boolean ? Base : Boolean;
			temp.style.display = DisplayStyle.None;

			Dirty();
		}
		/// <summary> 修改最小值 </summary>
		private void ModifyMin() { value.minValue = Min.value; Dirty(); }
		/// <summary> 修改最大值 </summary>
		private void ModifyMax() { value.maxValue = Max.value; Dirty(); }
		/// <summary> 修改默认值 </summary>
		private void ModifyValue() { value.baseValue = Base.value; Dirty(); }
		/// <summary> 修改布尔值 </summary>
		private void ModifyBoolean() { value.baseValue = Boolean.value ? value.maxValue : value.minValue; Dirty(); }

		/// <summary> 保存资源 </summary>
		private void Dirty() {
			EditorUtility.SetDirty(value);
			AssetDatabase.SaveAssets();
		}
	}
}
