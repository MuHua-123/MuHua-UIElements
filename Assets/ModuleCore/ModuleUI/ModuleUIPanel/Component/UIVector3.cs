using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// Vector3 - Panel
/// </summary>
public class UIVector3 : ModuleUIPanel {

	public Vector3 value;
	public UISliderInputH sliderX;
	public UISliderInputH sliderY;
	public UISliderInputH sliderZ;
	public event Action<Vector3> ValueChanged;

	public VisualElement SliderInputX => Q<VisualElement>("SliderInputX");
	public VisualElement SliderInputY => Q<VisualElement>("SliderInputY");
	public VisualElement SliderInputZ => Q<VisualElement>("SliderInputZ");

	public UIVector3(VisualElement element, VisualElement canvas) : base(element) {
		sliderX = new UISliderInputH(SliderInputX, canvas);
		sliderY = new UISliderInputH(SliderInputY, canvas);
		sliderZ = new UISliderInputH(SliderInputZ, canvas);

		sliderX.ValueChanged += SliderX_ValueChanged;
		sliderY.ValueChanged += SliderY_ValueChanged;
		sliderZ.ValueChanged += SliderZ_ValueChanged;
	}

	private void SliderX_ValueChanged(float obj) {
		value.x = obj;
		ValueChanged?.Invoke(value);
	}
	private void SliderY_ValueChanged(float obj) {
		value.y = obj;
		ValueChanged?.Invoke(value);
	}
	private void SliderZ_ValueChanged(float obj) {
		value.z = obj;
		ValueChanged?.Invoke(value);
	}
	public void Update() {
		sliderX.Update();
		sliderY.Update();
		sliderZ.Update();
	}
	/// <summary> 更新值 </summary>
	public void UpdateValue(Vector3 value) {
		this.value = value;
		sliderX.UpdateValue(value.x);
		sliderY.UpdateValue(value.y);
		sliderZ.UpdateValue(value.z);
	}
	/// <summary> 设置范围 </summary>
	public void Settings(Vector2 range) {
		sliderX.range = range;
		sliderY.range = range;
		sliderZ.range = range;
	}
}
