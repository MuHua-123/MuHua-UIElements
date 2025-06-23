using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 滚动视图 - 测试页面
/// </summary>
public class UIScrollViewTestPage : ModuleUIPage {

	private UIScrollViewV scrollView1;
	private UIScrollViewV scrollView2;
	private UIScrollViewH scrollView3;
	private UIScrollViewH scrollView4;
	private UIScrollView scrollView5;

	public VisualElement ScrollView1 => Q<VisualElement>("ScrollView1");
	public VisualElement ScrollView2 => Q<VisualElement>("ScrollView2");
	public VisualElement ScrollView3 => Q<VisualElement>("ScrollView3");
	public VisualElement ScrollView4 => Q<VisualElement>("ScrollView4");
	public VisualElement ScrollView5 => Q<VisualElement>("ScrollView5");

	public override VisualElement Element => root;

	private void Awake() {
		scrollView1 = new UIScrollViewV(ScrollView1, root, UIScrollViewV.UIDirection.FromTopToBottom);
		scrollView2 = new UIScrollViewV(ScrollView2, root, UIScrollViewV.UIDirection.FromBottomToTop);
		scrollView3 = new UIScrollViewH(ScrollView3, root, UIScrollViewH.UIDirection.FromLeftToRight);
		scrollView4 = new UIScrollViewH(ScrollView4, root, UIScrollViewH.UIDirection.FromRightToLeft);
		scrollView5 = new UIScrollView(ScrollView5, root);
	}

	private void Update() {
		scrollView1.Update();
		scrollView2.Update();
		scrollView3.Update();
		scrollView4.Update();
		scrollView5.Update();
	}
}
