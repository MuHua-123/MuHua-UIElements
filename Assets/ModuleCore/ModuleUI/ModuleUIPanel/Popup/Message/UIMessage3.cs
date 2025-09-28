using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 消息页面3
/// </summary>
public class UIMessage3 : ModuleUIPanel {

	private Action callback1;
	private Action callback2;

	public Label Content => Q<Label>("Content");
	public Button Button1 => Q<Button>("Button1");
	public Button Button2 => Q<Button>("Button2");

	public UIMessage3(VisualElement element) : base(element) {
		Button1.clicked += () => { callback1?.Invoke(); Settings(false); };
		Button2.clicked += () => { callback2?.Invoke(); Settings(false); };
	}

	public void Settings(bool active, string value = "", Action callback1 = null, Action callback2 = null) {
		element.EnableInClassList("document-page-hide", !active);
		Content.text = value;
		this.callback1 = callback1;
		this.callback2 = callback2;
	}
}
