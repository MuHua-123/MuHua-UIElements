using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 弹出窗口
/// </summary>
public class UIPopup : ModuleUIPanel {

	private Action callback;

	public VisualElement GamePopup => Q<VisualElement>("GamePopup");

	public VisualElement Top => Q<VisualElement>("Top");
	public Label Title => Top.Q<Label>("Title");
	public VisualElement Container => Top.Q<VisualElement>("Container");

	public VisualElement Bottom => Q<VisualElement>("Bottom");
	public VisualElement Button => Bottom.Q<VisualElement>("Button");

	public UIPopup(VisualElement element) : base(element) {
		Button.RegisterCallback<ClickEvent>((evt) => ButtonClick());
	}

	/// <summary> 按钮点击 </summary>
	public virtual void ButtonClick() {
		Settings(false);
		callback?.Invoke();
		callback = null;
	}

	/// <summary> 设置活动状态 </summary>
	public virtual void Settings(bool active, string content = "", Action callback = null) {
		GamePopup.EnableInClassList("gamepopup-hide", !active);
		if (!active) { return; }
		Title.text = content;
		this.callback = callback;
	}
}
