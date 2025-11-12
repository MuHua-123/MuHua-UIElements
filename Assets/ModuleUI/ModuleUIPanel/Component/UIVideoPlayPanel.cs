using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using MuHua;

/// <summary>
/// 视频播放控件
/// </summary>
public class UIVideoPlayPanel : ModuleUIPanel {
	private float showTime;
	private bool isDownSlider;
	private Action fullAction;
	private RenderTexture renderTexture;
	private VideoPlayer videoPlayer;
	private UISliderH slider;

	private VisualElement VideoView => element.Q<VisualElement>("VideoView");
	private VisualElement VideoController => element.Q<VisualElement>("VideoController");
	private VisualElement Slider => element.Q<VisualElement>("Slider");
	private Label Time => element.Q<Label>("Time");
	private Button Play => element.Q<Button>("Play");
	private Button Pause => element.Q<Button>("Pause");
	private Button FullScreen => element.Q<Button>("FullScreen");

	public UIVideoPlayPanel(VisualElement element, VisualElement canvas, VideoPlayer videoPlayer, Action fullAction = null) : base(element) {
		this.videoPlayer = videoPlayer;
		this.fullAction = fullAction;

		int width = (int)element.parent.resolvedStyle.width;
		int height = (int)element.parent.resolvedStyle.height;
		renderTexture = new RenderTexture(width, height, 0);
		Background background = Background.FromRenderTexture(renderTexture);
		VideoView.style.backgroundImage = new StyleBackground(background);

		Play.clicked += Play_clicked;
		Pause.clicked += Pause_clicked;
		FullScreen.clicked += FullScreen_clicked;

		VideoView.RegisterCallback<PointerDownEvent>((evt) => showTime = 5);
		VideoController.RegisterCallback<PointerDownEvent>((evt) => showTime = 5);

		Slider.RegisterCallback<PointerDownEvent>((evt) => isDownSlider = true);
		Slider.RegisterCallback<PointerUpEvent>((evt) => isDownSlider = false);
		Slider.RegisterCallback<PointerLeaveEvent>((evt) => isDownSlider = false);

		slider = new UISliderH(element, canvas);
		slider.ValueChanged += Slider_SlidingValueChanged;
	}

	/// <summary> 启用 </summary>
	public void Enable(string url) {
		videoPlayer.url = url; Enable();
	}
	/// <summary> 启用 </summary>
	public void Enable() {
		UpdateRenderTexture();
		videoPlayer.targetTexture = renderTexture;
		if (videoPlayer.isPlaying) { Play_clicked(); }
		else { Pause_clicked(); }
	}

	/// <summary> 更新 </summary>
	public void Update() {
		if (videoPlayer == null) { return; }
		showTime -= UnityEngine.Time.deltaTime;
		Visibility visibility = showTime > 0 ? Visibility.Visible : Visibility.Hidden;
		VideoController.style.visibility = visibility;
		//进度条
		float value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
		if (!isDownSlider) { slider.UpdateValue(value, false); }
		//播放时间
		string clockTime = TimeSpan.FromSeconds(videoPlayer.clockTime).ToString(@"mm\:ss");
		string length = TimeSpan.FromSeconds(videoPlayer.length).ToString(@"mm\:ss");
		Time.text = clockTime + "/" + length;
	}
	/// <summary> 播放 </summary>
	public void Play_clicked() {
		if (videoPlayer == null) { return; }
		videoPlayer.Play(); showTime = 5;
		Play.style.display = DisplayStyle.None;
		Pause.style.display = DisplayStyle.Flex;
	}
	/// <summary> 暂停 </summary>
	public void Pause_clicked() {
		if (videoPlayer == null) { return; }
		videoPlayer.Pause();
		Play.style.display = DisplayStyle.Flex;
		Pause.style.display = DisplayStyle.None;
	}

	/// <summary> 更新渲染纹理 </summary>
	private void UpdateRenderTexture() {
		int width = (int)element.parent.resolvedStyle.width;
		int height = (int)element.parent.resolvedStyle.height;
		if (renderTexture.width == width && renderTexture.height == height) { return; }
		renderTexture.Release();
		renderTexture = new RenderTexture(width, height, 0);
		Background background = Background.FromRenderTexture(renderTexture);
		VideoView.style.backgroundImage = new StyleBackground(background);
	}
	/// <summary> 全屏 </summary>
	private void FullScreen_clicked() {
		fullAction?.Invoke();
	}
	/// <summary> 进度条更新 </summary>
	private void Slider_SlidingValueChanged(float obj) {
		if (videoPlayer == null) { return; }
		videoPlayer.frame = (long)obj;
	}
}
