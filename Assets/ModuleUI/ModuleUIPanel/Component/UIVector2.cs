using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// Vector2 - Panel
/// </summary>
public class UIVector2 : ModuleUIPanel {

	public bool isLock;
	public Vector2 value;
	public UISliderInputH sliderX;
	public UISliderInputH sliderY;
	public event Action<Vector2, bool> ValueChanged;

	public Button Lock => Q<Button>("Lock");
	public VisualElement SliderX => Q<VisualElement>("SliderInputX");
	public VisualElement SliderY => Q<VisualElement>("SliderInputY");

	public UIVector2(VisualElement element, VisualElement canvas) : base(element) {
		sliderX = new UISliderInputH(SliderX, canvas);
		sliderY = new UISliderInputH(SliderY, canvas);

		Lock.clicked += Lock_clicked;
		sliderX.ValueChanged += SliderX_ValueChanged;
		sliderY.ValueChanged += SliderY_ValueChanged;
	}
	private void Lock_clicked() {
		isLock = !isLock;
		Lock.EnableInClassList("dashboard-button-s", isLock);
		if (isLock) { value.y = value.x; sliderY.UpdateValue(value.x); }
		ValueChanged?.Invoke(value, isLock);
	}
	private void SliderX_ValueChanged(float obj) {
		value.x = obj;
		if (isLock) { value.y = obj; sliderY.UpdateValue(obj); }
		ValueChanged?.Invoke(value, isLock);
	}
	private void SliderY_ValueChanged(float obj) {
		value.y = obj;
		if (isLock) { value.x = obj; sliderX.UpdateValue(obj); }
		ValueChanged?.Invoke(value, isLock);
	}
	public void Update() {
		sliderX.Update();
		sliderY.Update();
	}
	/// <summary> 更新值 </summary>
	public void UpdateValue(Vector2 value, bool isLock) {
		this.value = value;
		this.isLock = isLock;
		sliderX.UpdateValue(value.x);
		sliderY.UpdateValue(value.y);
		Lock.EnableInClassList("dashboard-button-s", isLock);
	}
	/// <summary> 设置范围 </summary>
	public void Settings(Vector2 range) {
		sliderX.range = range;
		sliderY.range = range;
	}
}
