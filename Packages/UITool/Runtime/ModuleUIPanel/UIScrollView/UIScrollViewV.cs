using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动视图 - 垂直
	/// </summary>
	public class UIScrollViewV : ModuleUIPanel, IDisposable, UIControl {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;
		/// <summary> 元素方向 </summary>
		public readonly UIDirection direction;
		/// <summary> 垂直滑块 </summary>
		public readonly UIScrollerV vertical;
		/// <summary> 值改变时 </summary>
		public event Action<float> ValueChanged;

		/// <summary>
		/// 方向
		/// </summary>
		public enum UIDirection {
			FromTopToBottom = 0,
			FromBottomToTop = 1,
		}

		public float value;
		public bool isDrag;
		public Vector3 originalPosition;
		public Vector3 pointerPosition;

		public readonly VisualElement Viewport;
		public readonly VisualElement Container;
		public readonly VisualElement ScrollerVertical;

		public UIScrollViewV(VisualElement element, VisualElement canvas, UIDirection direction = UIDirection.FromTopToBottom) : base(element) {
			this.canvas = canvas;
			this.direction = direction;

			Viewport = element.Children().FirstOrDefault(e => e.name == "Viewport");
			Container = Viewport.Children().FirstOrDefault(e => e.name == "Container");
			ScrollerVertical = element.Children().FirstOrDefault(e => e.name == "ScrollerVertical");

			if (direction == UIDirection.FromTopToBottom) { vertical = new UIScrollerV(ScrollerVertical, canvas, UIScrollerV.UIDirection.FromTopToBottom); }
			if (direction == UIDirection.FromBottomToTop) { vertical = new UIScrollerV(ScrollerVertical, canvas, UIScrollerV.UIDirection.FromBottomToTop); }

			// 设置事件
			vertical.ValueChanged += (y) => { UpdateValue(y); };

			Viewport.RegisterCallback<WheelEvent>(ViewportWheel);
			Viewport.RegisterCallback<PointerDownEvent>(DraggerDown);
			Viewport.RegisterCallback<MouseCaptureEvent>(DraggerUpOrLeave);
			Viewport.style.flexDirection = direction == UIDirection.FromTopToBottom ? FlexDirection.Column : FlexDirection.ColumnReverse;
			// 释放
			canvas.RegisterCallback<PointerUpEvent>(DraggerUpOrLeave);
			canvas.RegisterCallback<PointerLeaveEvent>(DraggerUpOrLeave);
			// 视图原始更新
			element.generateVisualContent += ElementGenerateVisualContent;
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
		/// <summary> 原始更新 </summary>
		private void ElementGenerateVisualContent(MeshGenerationContext context) {
			float height = Mathf.Clamp01(Viewport.resolvedStyle.height / Container.resolvedStyle.height);
			vertical.Dragger.style.height = Length.Percent(height * 100);
		}
		/// <summary> 滚轮滑动 </summary>
		private void ViewportWheel(WheelEvent evt) {
			float wheel = Mathf.Clamp(evt.delta.y, -1, 1);
			UpdateValue(value - wheel);
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

			float maxHeight = Viewport.resolvedStyle.height - Container.resolvedStyle.height;
			float y = offset.y / maxHeight;
			y *= direction == UIDirection.FromTopToBottom ? 1 : -1;
			UpdateValue(y);
		}
		/// <summary> 滑动弹性 </summary>
		private void SlidingElasticity() {
			float original = value;
			float max = Viewport.resolvedStyle.height < Container.resolvedStyle.height ? 1 : 0;
			if (value < 0) { value = Mathf.Lerp(value, 0, Time.deltaTime * 10); }
			if (value > max) { value = Mathf.Lerp(value, max, Time.deltaTime * 10); }
			if (original != value) { UpdateValue(value); }
		}
		/// <summary> 鼠标松开或离开 </summary>
		private void DraggerUpOrLeave(EventBase evt) {
			isDrag = false;
		}

		/// <summary> 更新状态 </summary>
		public virtual void Update() {
			vertical.Update();
			SlidingElasticity();
			if (isDrag) { Dragger(); }
		}
		/// <summary> 更新值(0-1) </summary>
		public virtual void UpdateValue(float value, bool send = true) {
			this.value = value;
			if (send) { ValueChanged?.Invoke(value); }
			float maxHeight = Viewport.resolvedStyle.height - Container.resolvedStyle.height;
			float position = maxHeight * value;
			position *= direction == UIDirection.FromTopToBottom ? 1 : -1;
			Container.transform.position = new Vector3(0, position);
			if (vertical.value != value) { vertical.UpdateValue(value, false); }
		}
	}
}