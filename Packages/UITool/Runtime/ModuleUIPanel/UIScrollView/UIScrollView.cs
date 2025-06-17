using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动视图
	/// </summary>
	public class UIScrollView : ModuleUIPanel, IDisposable {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;
		/// <summary> 水平滑块 </summary>
		public readonly UIScrollerH horizontal;
		/// <summary> 垂直滑块 </summary>
		public readonly UIScrollerV vertical;
		/// <summary> 值改变时 </summary>
		public event Action<Vector2> ValueChanged;

		public Vector2 value;
		public bool isDrag;
		public Vector3 originalPosition;
		public Vector3 pointerPosition;

		public readonly VisualElement Viewport;
		public readonly VisualElement Container;
		public readonly VisualElement ScrollerHorizontal;
		public readonly VisualElement ScrollerVertical;

		public UIScrollView(VisualElement element, VisualElement canvas) : base(element) {
			this.canvas = canvas;

			Viewport = element.Children().FirstOrDefault(e => e.name == "Viewport");
			Container = Viewport.Children().FirstOrDefault(e => e.name == "Container");
			ScrollerHorizontal = element.Children().FirstOrDefault(e => e.name == "ScrollerHorizontal");
			ScrollerVertical = element.Children().FirstOrDefault(e => e.name == "ScrollerVertical");

			element.generateVisualContent += ElementGenerateVisualContent;

			horizontal = new UIScrollerH(ScrollerHorizontal, canvas, UIScrollerH.UIDirection.FromLeftToRight);
			vertical = new UIScrollerV(ScrollerVertical, canvas, UIScrollerV.UIDirection.FromTopToBottom);

			// 设置事件
			horizontal.ValueChanged += (x) => { UpdateValue(new Vector2(x, value.y)); };
			vertical.ValueChanged += (y) => { UpdateValue(new Vector2(value.x, y)); };

			Viewport.RegisterCallback<WheelEvent>(ViewportWheel);
			Viewport.RegisterCallback<PointerDownEvent>(DraggerDown);
			Viewport.RegisterCallback<MouseCaptureEvent>(DraggerUpOrLeave);
			// 释放
			canvas.RegisterCallback<PointerUpEvent>(DraggerUpOrLeave);
			canvas.RegisterCallback<PointerLeaveEvent>(DraggerUpOrLeave);
		}
		/// <summary> 解绑事件，防止内存泄漏 </summary>
		public virtual void Dispose() {
			Viewport.UnregisterCallback<WheelEvent>(ViewportWheel);
			Viewport.UnregisterCallback<PointerDownEvent>(DraggerDown);
			Viewport.UnregisterCallback<MouseCaptureEvent>(DraggerUpOrLeave);
			canvas.UnregisterCallback<PointerUpEvent>(DraggerUpOrLeave);
			canvas.UnregisterCallback<PointerLeaveEvent>(DraggerUpOrLeave);
			element.generateVisualContent -= ElementGenerateVisualContent;
		}
		/// <summary> 视图原始更新 </summary>
		private void ElementGenerateVisualContent(MeshGenerationContext context) {
			float width = Mathf.Clamp01(Viewport.resolvedStyle.width / Container.resolvedStyle.width);
			horizontal.Dragger.style.width = Length.Percent(width * 100);
			float height = Mathf.Clamp01(Viewport.resolvedStyle.height / Container.resolvedStyle.height);
			vertical.Dragger.style.height = Length.Percent(height * 100);
		}
		/// <summary> 视图滚轮滑动 </summary>
		private void ViewportWheel(WheelEvent evt) {
			float x = Mathf.Clamp(evt.delta.x, -1, 1);
			float y = Mathf.Clamp(evt.delta.y, -1, 1);
			UpdateValue(new Vector2(value.x - x, value.y - y));
		}
		/// <summary> 拖拽按下 </summary>
		private void DraggerDown(PointerDownEvent evt) {
			isDrag = true;
			originalPosition = Container.transform.position;
			Vector3 mousePosition = UITool.GetMousePosition();
			pointerPosition = new Vector3(mousePosition.x, Screen.height - mousePosition.y);
		}
		/// <summary> 拖拽滑动 </summary>
		private void Dragger() {
			Vector3 mousePosition = UITool.GetMousePosition();
			Vector3 differ = new Vector3(mousePosition.x, Screen.height - mousePosition.y) - pointerPosition;
			Vector3 offset = differ + originalPosition;

			float maxWidth = Viewport.resolvedStyle.width - Container.resolvedStyle.width;
			float x = offset.x / maxWidth;
			float maxHeight = Viewport.resolvedStyle.height - Container.resolvedStyle.height;
			float y = offset.y / maxHeight;
			UpdateValue(new Vector2(x, y));
		}
		/// <summary> 滑动弹性 </summary>
		private void SlidingElasticity() {
			Vector2 original = value;
			float maxX = Viewport.resolvedStyle.width < Container.resolvedStyle.width ? 1 : 0;
			if (value.x < 0) { value.x = Mathf.Lerp(value.x, 0, Time.deltaTime * 10); }
			if (value.x > maxX) { value.x = Mathf.Lerp(value.x, maxX, Time.deltaTime * 10); }

			float maxY = Viewport.resolvedStyle.height < Container.resolvedStyle.height ? 1 : 0;
			if (value.y < 0) { value.y = Mathf.Lerp(value.y, 0, Time.deltaTime * 10); }
			if (value.y > maxY) { value.y = Mathf.Lerp(value.y, maxY, Time.deltaTime * 10); }

			if (original != value) { UpdateValue(value); }
		}
		/// <summary> 鼠标松开或离开 </summary>
		private void DraggerUpOrLeave(EventBase evt) {
			isDrag = false;
		}

		/// <summary> 更新状态 </summary>
		public virtual void Update() {
			horizontal.Update();
			vertical.Update();
			SlidingElasticity();
			if (isDrag) { Dragger(); }
		}
		/// <summary> 更新值(0-1) </summary>
		public void UpdateValue(Vector2 value, bool send = true) {
			this.value = value;
			if (send) { ValueChanged?.Invoke(value); }
			float maxWidth = Viewport.resolvedStyle.width - Container.resolvedStyle.width;
			float x = maxWidth * value.x;
			float maxHeight = Viewport.resolvedStyle.height - Container.resolvedStyle.height;
			float y = maxHeight * value.y;
			Container.transform.position = new Vector3(x, y);

			if (horizontal.value != value.x) { horizontal.UpdateValue(value.x, false); }
			if (vertical.value != value.y) { vertical.UpdateValue(value.y, false); }
		}
	}
}