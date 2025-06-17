using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动条 - 水平
	/// </summary>
	public class UIScrollerH : ModuleUIPanel, IDisposable {
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
			FromLeftToRight = 0,
			FromRightToLeft = 1,
		}

		public float value;
		public bool isDragger;
		public float originalPosition;
		public float pointerPosition;

		public readonly VisualElement Dragger;

		public UIScrollerH(VisualElement element, VisualElement canvas, UIDirection direction = UIDirection.FromLeftToRight) : base(element) {
			this.canvas = canvas;
			this.direction = direction;

			Dragger = Q<VisualElement>("Dragger");

			element.style.flexDirection = direction == UIDirection.FromLeftToRight ? FlexDirection.Row : FlexDirection.RowReverse;

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
			originalPosition = Dragger.transform.position.x;
			pointerPosition = UITool.GetMousePosition().x;
		}
		/// <summary> 元素按下 </summary>
		private void ElementDown(PointerDownEvent evt) {
			float offset = evt.localPosition.x - Dragger.resolvedStyle.width * 0.5f;
			float max = element.resolvedStyle.width - Dragger.resolvedStyle.width;
			float value1 = Mathf.InverseLerp(0, max, offset);
			float value2 = Mathf.InverseLerp(max, 0, offset);
			float value = direction == UIDirection.FromLeftToRight ? value1 : value2;
			UpdateValue(value);
		}
		/// <summary> 鼠标松开或离开 </summary>
		private void DraggerUpOrLeave(EventBase evt) {
			isDragger = false;
		}

		/// <summary> 更新状态 </summary>
		public void Update() {
			if (!isDragger) { return; }
			float differ = UITool.GetMousePosition().x - pointerPosition;
			float offset = differ + originalPosition;
			offset *= direction == UIDirection.FromLeftToRight ? 1 : -1;
			float max = element.resolvedStyle.width - Dragger.resolvedStyle.width;
			float value = Mathf.InverseLerp(0, max, offset);
			UpdateValue(value);
		}
		/// <summary> 更新值(0-1) </summary>
		public void UpdateValue(float value, bool send = true) {
			this.value = value;
			if (send) { ValueChanged?.Invoke(value); }
			float max = element.resolvedStyle.width - Dragger.resolvedStyle.width;
			float position = Mathf.Lerp(0, max, value);
			position *= direction == UIDirection.FromLeftToRight ? 1 : -1;
			Dragger.transform.position = new Vector2(position, 0);
		}
	}
}
