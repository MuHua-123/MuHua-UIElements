using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动条 - 垂直
	/// </summary>
	public class UIScrollerV : ModuleUIPanel, IDisposable, UIControl {
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

		public readonly VisualElement Dragger;

		public UIScrollerV(VisualElement element, VisualElement canvas, UIDirection direction = UIDirection.FromTopToBottom) : base(element) {
			this.canvas = canvas;
			this.direction = direction;

			Dragger = Q<VisualElement>("Dragger");

			element.style.flexDirection = direction == UIDirection.FromTopToBottom ? FlexDirection.Column : FlexDirection.ColumnReverse;

			//设置事件
			Dragger.RegisterCallback<PointerDownEvent>(DraggerDown);
			element.RegisterCallback<PointerDownEvent>(ElementDown);

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
			originalPosition = Dragger.transform.position.y;
			pointerPosition = Screen.height - UITool.GetMousePosition().y;
		}
		/// <summary> 元素按下 </summary>
		private void ElementDown(PointerDownEvent evt) {
			float offset = evt.localPosition.y - Dragger.resolvedStyle.height * 0.5f;
			float max = element.resolvedStyle.height - Dragger.resolvedStyle.height;
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
			offset *= direction == UIDirection.FromTopToBottom ? 1 : -1;
			float max = element.resolvedStyle.height - Dragger.resolvedStyle.height;
			float value = Mathf.InverseLerp(0, max, offset);
			UpdateValue(value);
		}
		/// <summary> 更新值(0-1) </summary>
		public void UpdateValue(float value, bool send = true) {
			this.value = value;
			if (send) { ValueChanged?.Invoke(value); }
			float max = element.resolvedStyle.height - Dragger.resolvedStyle.height;
			float position = Mathf.Lerp(0, max, value);
			position *= direction == UIDirection.FromTopToBottom ? 1 : -1;
			Dragger.transform.position = new Vector2(0, position);
		}
	}
}