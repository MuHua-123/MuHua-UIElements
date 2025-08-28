using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滑块 - 垂直
	/// </summary>
	public class UISliderV : ModuleUIPanel, IDisposable, UIControl {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;
		/// <summary> 元素方向 </summary>
		public readonly UIDirection direction;
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
		public bool isDragger;
		public float originalPosition;
		public float pointerPosition;

		public readonly Label Title;
		public readonly VisualElement Container;
		public readonly VisualElement Tracker;
		public readonly VisualElement Dragger;

		public UISliderV(VisualElement element, VisualElement canvas, UIDirection direction = UIDirection.FromTopToBottom) : base(element) {
			this.canvas = canvas;
			this.direction = direction;

			Title = Q<Label>("Title");
			Container = Q<VisualElement>("Container");
			Tracker = Q<VisualElement>("Tracker");
			Dragger = Q<VisualElement>("Dragger");

			//设置事件
			Dragger.RegisterCallback<PointerDownEvent>(DraggerDown);
			Container.RegisterCallback<PointerDownEvent>(ElementDown);

			canvas.RegisterCallback<PointerUpEvent>(DraggerUpOrLeave);
			canvas.RegisterCallback<PointerLeaveEvent>(DraggerUpOrLeave);
		}
		/// <summary> 解绑事件，防止内存泄漏 </summary>
		public void Dispose() {
			Dragger.UnregisterCallback<PointerDownEvent>(DraggerDown);
			element.UnregisterCallback<PointerDownEvent>(ElementDown);
			canvas.UnregisterCallback<PointerUpEvent>(DraggerUpOrLeave);
			canvas.UnregisterCallback<PointerLeaveEvent>(DraggerUpOrLeave);
		}
		/// <summary> 拖拽按下 </summary>
		private void DraggerDown(PointerDownEvent evt) {
			isDragger = true;
			float value1 = Tracker.resolvedStyle.height;
			float value2 = Container.resolvedStyle.height - Tracker.resolvedStyle.height;
			originalPosition = direction == UIDirection.FromTopToBottom ? value1 : value2;
			pointerPosition = Screen.height - UITool.GetMousePosition().y;
		}
		/// <summary> 元素按下 </summary>
		private void ElementDown(PointerDownEvent evt) {
			float offset = evt.localPosition.y;
			float max = Container.resolvedStyle.height;
			float value1 = Mathf.InverseLerp(0, max, offset);
			float value2 = Mathf.InverseLerp(max, 0, offset);
			float value = direction == UIDirection.FromTopToBottom ? value1 : value2;
			UpdateValue(value);
		}
		/// <summary> 鼠标松开或离开 </summary>
		private void DraggerUpOrLeave(EventBase evt) {
			isDragger = false;
		}

		/// <summary> 更新状态 </summary>
		public void Update() {
			if (!isDragger) { return; }
			float differ = Screen.height - UITool.GetMousePosition().y - pointerPosition;
			float offset = differ + originalPosition;
			float max = Container.resolvedStyle.height;
			float value1 = Mathf.InverseLerp(0, max, offset);
			float value2 = Mathf.InverseLerp(max, 0, offset);
			float value = direction == UIDirection.FromTopToBottom ? value1 : value2;
			UpdateValue(value);
		}
		/// <summary> 更新值(0-1) </summary>
		public void UpdateValue(float obj, bool send = true) {
			value = (float)Math.Round(obj, 3);
			if (send) { ValueChanged?.Invoke(value); }
			Tracker.style.height = Length.Percent(value * 100);
		}
	}
}