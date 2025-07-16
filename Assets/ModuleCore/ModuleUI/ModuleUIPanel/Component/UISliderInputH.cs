using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// SliderInputH - Panel
/// </summary>
public class UISliderInputH : ModuleUIPanel {

	public float value;
	public Vector2 range = new Vector2(1, 1);
	public UISliderH slider;
	public event Action<float> ValueChanged;

	public VisualElement Slider => Q<VisualElement>("Slider");
	public UIFloatField FloatField => Q<UIFloatField>("UIFloatField");

	public UISliderInputH(VisualElement element, VisualElement canvas) : base(element) {
		slider = new UISliderH(element, canvas);

		slider.ValueChanged += Slider_ValueChanged;
		FloatField.RegisterCallback<ChangeEvent<float>>(FloatField_ValueChanged);
	}
	private void Slider_ValueChanged(float obj) {
		value = Mathf.Lerp(range.x, range.y, obj);
		FloatField.SetValueWithoutNotify(value);
		ValueChanged?.Invoke(value);
	}
	private void FloatField_ValueChanged(ChangeEvent<float> obj) {
		value = Mathf.Clamp(obj.newValue, range.x, range.y);
		float scale = Mathf.InverseLerp(range.x, range.y, value);
		slider.UpdateValue(scale, false);
		ValueChanged?.Invoke(value);
	}
	public void Update() {
		slider.Update();
	}
	/// <summary> 更新值 </summary>
	public void UpdateValue(float value) {
		this.value = Mathf.Clamp(range.x, range.y, value);
		float scale = Mathf.InverseLerp(range.x, range.y, value);
		slider.UpdateValue(scale, false);
		FloatField.SetValueWithoutNotify(value);
	}
}
