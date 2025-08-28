using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 悬浮窗口
	/// </summary>
	public abstract class UIWindow : ModuleUIPanel, IDisposable, UIControl {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;
		/// <summary> 隐藏的USS类名 </summary>
		public string hideClassName = "document-page-hide";

		private bool isDownMove;
		private Vector3 pointerPosition;
		private Vector3 originalPosition;

		public VisualElement Window => Q<VisualElement>("Window");
		public VisualElement Top => Q<VisualElement>("Top");
		public VisualElement Container => Q<VisualElement>("Container");

		public Label Title => Q<Label>("Title");
		public VisualElement Close => Q<VisualElement>("Close");

		public UIWindow(VisualElement element, VisualElement canvas) : base(element) {
			this.canvas = canvas;

			Top.RegisterCallback<PointerDownEvent>(TopDown);
			canvas.RegisterCallback<PointerUpEvent>(DraggerUpOrLeave);
			canvas.RegisterCallback<PointerLeaveEvent>(DraggerUpOrLeave);
			Close.RegisterCallback<ClickEvent>(CloseButton);
		}
		/// <summary> 解绑事件，防止内存泄漏 </summary>
		public virtual void Dispose() {
			Top.UnregisterCallback<PointerDownEvent>(TopDown);
			canvas.UnregisterCallback<PointerUpEvent>(DraggerUpOrLeave);
			canvas.UnregisterCallback<PointerLeaveEvent>(DraggerUpOrLeave);
			Close.UnregisterCallback<ClickEvent>(CloseButton);
		}

		/// <summary> 按下Top </summary>
		private void TopDown(PointerDownEvent evt) {
			isDownMove = true;
			pointerPosition = UITool.GetMousePosition();
			originalPosition = Window.transform.position;
		}
		/// <summary> 鼠标松开或离开 </summary>
		private void DraggerUpOrLeave(EventBase evt) {
			isDownMove = false;
		}
		/// <summary> 关闭按钮 </summary>
		private void CloseButton(EventBase evt) {
			Settings(false);
		}

		/// <summary> 设置活动状态 </summary>
		public virtual void Settings(bool active) {
			element.EnableInClassList(hideClassName, !active);
		}

		/// <summary> 更新状态 </summary>
		public virtual void Update() {
			if (!isDownMove) { return; }
			Vector3 mousePosition = UITool.GetMousePosition();
			Vector3 offset = mousePosition - pointerPosition;
			Vector3 position = originalPosition + new Vector3(offset.x, -offset.y);

			float width = canvas.resolvedStyle.width - Window.resolvedStyle.width;
			float height = canvas.resolvedStyle.height - Window.resolvedStyle.height;
			position.x = Mathf.Clamp(position.x, 0, width);
			position.y = Mathf.Clamp(position.y, 0, height);

			Window.transform.position = position;
		}

	}
}

