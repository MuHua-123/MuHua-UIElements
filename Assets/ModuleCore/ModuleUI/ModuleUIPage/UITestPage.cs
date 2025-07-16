using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 测试页面
/// </summary>
public class UITestPage : ModuleUIPage {

	public VisualTreeAsset SlideButtonTemplate;

	public UISlideButton<UIOption, Option> slideButton;

	public override VisualElement Element => root;
	public VisualElement SlideButton => Q<VisualElement>("SlideButton");

	public void Awake() {
		slideButton = new UISlideButton<UIOption, Option>(SlideButton, root, SlideButtonTemplate, (data, element) => new UIOption(data, element));

		List<Option> options = new List<Option>();
		options.Add(new Option());
		options.Add(new Option());
		options.Add(new Option());
		options.Add(new Option());
		slideButton.Create(options);

		slideButton.Items[1].Select();
	}
	public void Update() {
		slideButton.Update();
	}

	public class Option : DataSlideButton {

	}

	public class UIOption : ModuleUIItem<Option> {

		public Button Button => Q<Button>("Button");

		public UIOption(Option value, VisualElement element) : base(value, element) {
			value.element = element;
			Button.clicked += Select;
		}
		public override void SelectState() {
			Button.EnableInClassList("slidebutton-button-s", true);
		}
		public override void DefaultState() {
			Button.EnableInClassList("slidebutton-button-s", false);
		}
	}
}
