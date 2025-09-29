using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 属性 - UI列表
	/// </summary>
	public class UIAttributeList : ModuleUIPanel, UIControl {
		/// <summary> 拖拽预览 </summary>
		public VisualElement preview;
		/// <summary> 鼠标位置 </summary>
		public Vector2 mousePosition;
		/// <summary> 偏移位置 </summary>
		public Vector2 offsetPosition;
		/// <summary> 项目列表 </summary>
		public ModuleUIItems<UIItem, AttributeInstanceConst> items;

		public UIAttributeList(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
			items = new ModuleUIItems<UIItem, AttributeInstanceConst>(element, templateAsset,
			(data, element) => new UIItem(data, element, this));

			element.RegisterCallback<MouseMoveEvent>(FloatingFunc);
		}
		private void FloatingFunc(MouseMoveEvent evt) {
			mousePosition = evt.mousePosition;
		}

		public void Update() {
			if (preview == null) { return; }
			preview.transform.position = mousePosition - offsetPosition;
		}

		public void Dispose() => items.Release();

		/// <summary> 初始化 </summary>
		public void Initial(List<AttributeInstanceConst> list) {
			items.Create(list);
		}
		/// <summary> 设置值 </summary>
		// public void Settings(T value) => callback?.Invoke(value);

		public void MouseDownEvent(VisualElement element) {
			preview = element;
			offsetPosition = mousePosition;
			// Debug.Log($"{preview.worldBound.position} - {mousePosition}");
			// offsetPosition = preview.worldBound.position - mousePosition;
			// Debug.Log($"{preview.worldBound.position} = {mousePosition} + {offsetPosition}");
		}
		public void MouseUpEvent() {
			if (preview == null) { return; }
			preview.transform.position = Vector2.zero;

			int insertIndex = GetInsertIndex(element, preview);
			int oldIndex = element.IndexOf(preview);

			// 修正插入索引
			// if (insertIndex > oldIndex) insertIndex--;
			Debug.Log($"插入位置 {insertIndex} - 旧位置 {oldIndex}");
			// 重新排序UI和数据
			if (oldIndex != insertIndex) { MovePreviewElement(oldIndex, insertIndex); }
			preview = null;
		}

		/// <summary>
		/// 获取插入索引
		/// </summary>
		private int GetInsertIndex(VisualElement parent, VisualElement preview) {
			Vector2 previewCenter = preview.worldBound.center;
			int insertIndex = 0;
			for (int i = 0; i < parent.childCount; i++) {
				var child = parent[i];
				if (child == preview) continue;
				if (previewCenter.y < child.worldBound.center.y) {
					insertIndex = i;
					break;
				}
				insertIndex = i + 1;
			}
			return insertIndex;
		}

		/// <summary>
		/// 移动预览元素到新位置
		/// </summary>
		private void MovePreviewElement(int oldIndex, int newIndex) {
			VisualElement temp = element[oldIndex];
			// element.RemoveAt(oldIndex);
			element.Insert(newIndex, temp);
		}

		/// <summary> UI项 </summary>
		public class UIItem : ModuleUIItem<AttributeInstanceConst> {
			public readonly UIAttributeList parent;

			public Label Range => Q<Label>("Range");
			public TextField Name => Q<TextField>("Name");
			public EnumField Type => Q<EnumField>("Type");
			public FloatField Value => Q<FloatField>("Value");

			public UIItem(AttributeInstanceConst value, VisualElement element, UIAttributeList parent) : base(value, element) {
				this.parent = parent;
				Name.SetValueWithoutNotify(value.name);
				Type.Init(value.type);
				Type.SetValueWithoutNotify(value.type);
				Value.SetValueWithoutNotify(value.defaultValue);
				Range.text = $"{value.minValue} ~ {value.maxValue}";
				element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
				element.RegisterCallback<MouseUpEvent>(MouseUpEvent);
			}
			public override void DefaultState() {
				// Button.EnableInClassList("button-s", false);
			}
			public override void SelectState() {
				// parent.Settings(value);
				// Button.EnableInClassList("button-s", true);
			}

			private void MouseDownEvent(MouseDownEvent evt) => parent.MouseDownEvent(element);

			private void MouseUpEvent(MouseUpEvent evt) => parent.MouseUpEvent();
		}
	}
}
