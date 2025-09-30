using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using MuHua;

/// <summary>
/// 属性面板
/// </summary>
public class UIAttributePanel : ModuleUIPanel {

	private AttributeInstanceConst value;

	public Label Range => Q<Label>("Range");
	public TextField Name => Q<TextField>("Name");
	public EnumField Type => Q<EnumField>("Type");
	public FloatField Value => Q<FloatField>("Value");
	public Toggle Boolean => Q<Toggle>("Boolean");

	public UIAttributePanel(AttributeInstanceConst value, VisualElement element) : base(element) {
		this.value = value;
		Initial();
		Name.RegisterCallback<FocusOutEvent>(evt => ModifyName());
		Type.RegisterValueChangedCallback(evt => ModifyType((AttributeType)evt.newValue));
		Value.RegisterCallback<FocusOutEvent>(evt => ModifyValue());
		Boolean.RegisterValueChangedCallback(evt => ModifyBoolean());
	}

	public void Initial() {
		Name.SetValueWithoutNotify(value.name);
		Type.Init(value.type);
		Value.SetValueWithoutNotify(value.defaultValue);
		Boolean.value = value.defaultValue > 0;

		if (value.type == AttributeType.Boolean) {
			Value.style.display = DisplayStyle.None;
		}
		else { Boolean.style.display = DisplayStyle.None; }

		if (value.type == AttributeType.Percentage) {
			Range.text = $"{value.minValue}% ~ {value.maxValue}%";
		}
		else { Range.text = $"{value.minValue} ~ {value.maxValue}"; }
	}

	/// <summary> 修改名字 </summary>
	private void ModifyName() { value.name = Name.value; Dirty(); }
	/// <summary> 修改类型 </summary>
	private void ModifyType(AttributeType type) {
		if (type == AttributeType.Float) { value.InitialFloat(); }
		if (type == AttributeType.Integer) { value.InitialInteger(); }
		if (type == AttributeType.Boolean) { value.InitialBoolean(); }
		if (type == AttributeType.Percentage) { value.InitialPercentage(); }
		if (value.type == AttributeType.Boolean) {
			Value.style.display = DisplayStyle.None;
		}
		else { Boolean.style.display = DisplayStyle.None; }
		Dirty();
	}
	/// <summary> 修改默认值 </summary>
	private void ModifyValue() { value.defaultValue = Value.value; Dirty(); }
	/// <summary> 修改布尔值 </summary>
	private void ModifyBoolean() { value.defaultValue = Boolean.value ? 1 : 0; Dirty(); }

	/// <summary> 保存资源 </summary>
	private void Dirty() {
		EditorUtility.SetDirty(value);
		AssetDatabase.SaveAssets();
	}
}
