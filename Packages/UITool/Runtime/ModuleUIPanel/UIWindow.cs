using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 悬浮窗口
	/// </summary>
	public abstract class UIWindow : ModuleUIPanel {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;

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
			canvas.RegisterCallback<PointerUpEvent>((evt) => isDownMove = false);
			canvas.RegisterCallback<PointerLeaveEvent>((evt) => isDownMove = false);

			Close.RegisterCallback<ClickEvent>((evt) => SetActive(false));
		}

		/// <summary> 按下Top </summary>
		private void TopDown(PointerDownEvent evt) {
			isDownMove = true;
			pointerPosition = UITool.GetMousePosition();
			originalPosition = Window.transform.position;
		}

		/// <summary> 设置活动状态 </summary>
		public virtual void SetActive(bool active) {
			Window.EnableInClassList("window-hidden", !active);
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

