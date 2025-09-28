using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 指针提示
/// </summary>
public class UIGuidance : ModuleUIPanel {

	private bool isDown;
	private Action callback;
	private Action<bool> ValueChanged;
	private Vector2 offset;
	private Vector3 originalPosition;
	private Vector3 pointerPosition;
	private VisualElement target;
	private UIToggle toggle;

	public Label Prompt => Q<Label>("Prompt");
	public VisualElement Toggle => Q<VisualElement>("Toggle");
	public VisualElement Button => Q<VisualElement>("Button");
	public VisualElement Pointer => Q<VisualElement>("Pointer");

	public UIGuidance(VisualElement element) : base(element) {
		toggle = new UIToggle(Toggle);
		toggle.ValueChanged += (value) => { ValueChanged?.Invoke(value); };

		Button.RegisterCallback<ClickEvent>(ClickEvent);

		element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
		element.RegisterCallback<MouseUpEvent>(MouseUpEvent);
	}

	public void Update() {
#if UNITY_EDITOR
		if (target == null) { return; }
		if (isDown) {
			Vector3 mousePosition = UITool.GetMousePosition();
			Vector3 differ = new Vector3(mousePosition.x, Screen.height - mousePosition.y) - pointerPosition;

			Pointer.transform.position = originalPosition + differ;
		}
		else {
			Pointer.transform.position = target.worldBound.position + offset;
		}
#else
		if (target == null) { return; }
		Pointer.transform.position = target.worldBound.position + offset;
#endif
	}

	private void ClickEvent(ClickEvent evt) {
		element.EnableInClassList("document-page-hide", true);
		callback?.Invoke();
	}
	private void MouseDownEvent(MouseDownEvent evt) {
#if UNITY_EDITOR
		isDown = true;
		originalPosition = Pointer.transform.position;
		Vector3 mousePosition = UITool.GetMousePosition();
		pointerPosition = new Vector3(mousePosition.x, Screen.height - mousePosition.y);
#endif
	}
	private void MouseUpEvent(MouseUpEvent evt) {
		isDown = false;
		float x = Pointer.transform.position.x - target.worldBound.position.x;
		float y = Pointer.transform.position.y - target.worldBound.position.y;
		Vector3 offset = new Vector3(x, y);
		Debug.Log(offset);
	}

	/// <summary> 打开提示 </summary>
	public void Settings(string content, VisualElement target, Vector2 offset, Action callback) {
		this.target = target;
		this.offset = offset;
		this.callback = callback;
		Prompt.text = content;
		element.EnableInClassList("document-page-hide", false);
	}
	/// <summary> 设置提示 </summary>
	public void Settings(bool value, Action<bool> ValueChanged) {
		this.ValueChanged = ValueChanged;
		toggle.UpdateValue(value, false);
	}
}
/// <summary>
/// 图案页指引
/// </summary>
public class UIPatternPageGuidance : ModuleUIPanel {

	private bool isNot;

	public VisualElement Top => Q<VisualElement>("Top");
	public Button Button1 => Top.Q<Button>("Button1");// 创建图案 
	public Button Button4 => Top.Q<Button>("Button4");// 创建元素

	public VisualElement Dashboard => Q<VisualElement>("Dashboard");
	public VisualElement PatternLibrary => Q<VisualElement>("PatternLibrary");
	public VisualElement TemplateLibrary => Q<VisualElement>("TemplateLibrary");

	public UIPatternPageGuidance(VisualElement element) : base(element) { }

	public void Guidance1() {
		if (isNot) { return; }
		string content = "第一步 点击【创建图案】按钮，即可新建一个空白画板，开始您的设计。";
		Guidance(content, Button1, new Vector2(140, -55), Guidance2);
	}
	public void Guidance2() {
		string content = "第二步 提供两种创作方式 \n1，选用模板：点击【图案模板】，选择模板后参考其结构与配色，进行纹样重构与创新设计。";
		Guidance(content, TemplateLibrary, new Vector2(310, 0), Guidance3);
	}
	public void Guidance3() {
		string content = "2，全新创作：点击【创建元素】创建空白图案框，自行选择【图案元素库】内的图案素材进行创新设计。";
		Guidance(content, Button4, new Vector2(-530, -65), Guidance4);
	}
	public void Guidance4() {
		string content = "导入个人素材：点击【自定义】-【导入素材】，导入后请点击【自定义】刷新以更新显示。";
		Guidance(content, PatternLibrary, new Vector2(310, 0), Guidance5);
	}
	public void Guidance5() {
		string content = "图案编辑功能：选中图案元素，可调整其颜色、位置、大小、镜像、连续排列等操作。\n画布设置：如设计透明背景的图案，可根据图案色彩风格自定义画板颜色，便于更直观地进行设计。";
		Guidance(content, Dashboard, new Vector2(-530, 0), null);
	}
	public void Guidance(string content, VisualElement visual, Vector3 offset, Action action) {
		// UIGuidance guidance = ModuleUI.I.popupManager.guidance;
		// guidance.Settings(content, visual, offset, action);
		// guidance.Settings(isNot, (value) => { isNot = value; });
	}
}