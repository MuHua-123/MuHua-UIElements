using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 滑动条 - 测试页面
/// </summary>
public class UISliderTestPage : ModuleUIPage {

	private UISliderH sliderH1;
	private UISliderH sliderH2;
	private UISliderV sliderV1;
	private UISliderV sliderV2;

	public VisualElement SliderH1 => Q<VisualElement>("SliderH1");
	public VisualElement SliderH2 => Q<VisualElement>("SliderH2");
	public VisualElement SliderV1 => Q<VisualElement>("SliderV1");
	public VisualElement SliderV2 => Q<VisualElement>("SliderV2");

	public override VisualElement Element => root;

	private void Awake() {
		sliderH1 = new UISliderH(SliderH1, root, UISliderH.UIDirection.FromLeftToRight);
		sliderH2 = new UISliderH(SliderH2, root, UISliderH.UIDirection.FromRightToLeft);
		sliderV1 = new UISliderV(SliderV1, root, UISliderV.UIDirection.FromTopToBottom);
		sliderV2 = new UISliderV(SliderV2, root, UISliderV.UIDirection.FromBottomToTop);
	}
	private void Update() {
		sliderH1.Update();
		sliderH2.Update();
		sliderV1.Update();
		sliderV2.Update();
	}
}
