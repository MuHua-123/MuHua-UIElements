using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 加载进度
/// </summary>
public class UIProgres : ModuleUIPanel {

	public UISliderH progress;

	public VisualElement Progress => element.Q<VisualElement>("Progress");
	public Label Title => Progress.Q<Label>("Title");

	public UIProgres(VisualElement element) : base(element) {
		progress = new UISliderH(Progress, element);
	}

	/// <summary>
	/// 设置进度（0-1）
	/// </summary>
	/// <param name="value"></param>
	public void Settings(bool active, string content = "", float value = 0) {
		Title.text = content;
		progress.UpdateValue(value, false);
		element.EnableInClassList("document-page-hide", !active);
	}
}
