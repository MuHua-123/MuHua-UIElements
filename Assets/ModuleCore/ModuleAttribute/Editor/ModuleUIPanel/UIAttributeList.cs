using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 属性 - UI列表
	/// </summary>
	public class UIAttributeList : ModuleUIPanel, UIControl {
		/// <summary> 模板资源 </summary>
		public VisualTreeAsset templateAsset;
		/// <summary> 移动目标 </summary>
		public VisualElement moving;
		/// <summary> 拖拽预览 </summary>
		public VisualElement preview;
		/// <summary> 项目预览 </summary>
		public UIItem previewItem;
		/// <summary> 鼠标偏移 </summary>
		public Vector2 mousePosition;
		/// <summary> 位置偏移 </summary>
		public Vector2 positionOffset;
		/// <summary> 偏移位置 </summary>
		public Vector2 mouseOffset;
		/// <summary> 属性列表 </summary>
		public AttributeContainerConst container;
		/// <summary> 项目列表 </summary>
		public ModuleUIItems<UIItem, AttributeInstanceConst> items;

		public UIAttributeList(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
			this.templateAsset = templateAsset;
			items = new ModuleUIItems<UIItem, AttributeInstanceConst>(element, templateAsset,
			(data, element) => new UIItem(data, element, this));

			element.RegisterCallback<MouseUpEvent>(MouseUpEvent);
			element.RegisterCallback<MouseMoveEvent>(FloatingFunc);
		}
		private void MouseUpEvent(MouseUpEvent evt) {
			if (preview == null) { return; }
			preview.parent.Remove(preview);
			preview = null;
			moving.EnableInClassList("acce-hide", false);
			if (previewItem == null) { return; }
			previewItem.Dispose();

			EditorUtility.SetDirty(container);
			AssetDatabase.SaveAssets();
		}
		private void FloatingFunc(MouseMoveEvent evt) {
			mousePosition = evt.mousePosition;
		}
		public void Update() {
			if (preview == null) { return; }
			preview.transform.position = positionOffset + mousePosition - mouseOffset;
			int insertIndex = GetInsertIndex(element, preview);
			int oldIndex = element.IndexOf(moving);
			// 修正插入索引
			// Debug.Log($"{insertIndex} , {oldIndex}");
			if (insertIndex > oldIndex) insertIndex--;
			if (oldIndex != insertIndex) { MovePreviewElement(oldIndex, insertIndex); }
		}
		public void Dispose() => items.Release();

		/// <summary> 初始化 </summary>
		public void Initial(AttributeContainerConst container) {
			this.container = container;
			items.Create(container.instances);
		}
		/// <summary> 设置值 </summary>
		public void Settings(VisualElement moving, AttributeInstanceConst value) {
			this.moving = moving;
			moving.EnableInClassList("acce-hide", true);

			preview = templateAsset.Instantiate();
			previewItem = new UIItem(value, preview);
			items.element.Add(preview);

			preview.EnableInClassList("acce-preview", true);
			// Debug.Log($"{preview.transform.position} , {preview.worldBound.position} , {moving.worldBound.position}");
			mouseOffset = mousePosition;
			// preview.transform.position的默认值
			positionOffset = moving.worldBound.position - preview.worldBound.position;
		}

		/// <summary> 获取插入索引 </summary>
		private int GetInsertIndex(VisualElement parent, VisualElement preview) {
			Vector2 previewCenter = preview.worldBound.center;
			int insertIndex = 0;
			for (int i = 0; i < parent.childCount - 1; i++) {
				VisualElement child = parent[i];
				if (child == preview) continue;
				if (previewCenter.y - 10 < child.worldBound.center.y) { insertIndex = i; break; }
				insertIndex = i + 1;
			}
			return insertIndex;
		}
		/// <summary> 移动预览元素到新位置 </summary>
		private void MovePreviewElement(int oldIndex, int newIndex) {
			VisualElement temp = element[oldIndex];
			element.Insert(newIndex, temp);

			AttributeInstanceConst instanceConst = container.instances[oldIndex];
			container.instances.RemoveAt(oldIndex);
			container.instances.Insert(newIndex, instanceConst);
		}

		/// <summary> UI项 </summary>
		public class UIItem : ModuleUIItem<AttributeInstanceConst> {
			public readonly UIAttributeList parent;
			public UIAttributePanel attributePanel;

			public UIItem(AttributeInstanceConst value, VisualElement element) : base(value, element) {
				attributePanel = new UIAttributePanel(value, element);
			}
			public UIItem(AttributeInstanceConst value, VisualElement element, UIAttributeList parent) : base(value, element) {
				this.parent = parent;
				attributePanel = new UIAttributePanel(value, element);
				element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
			}
			public override void DefaultState() {
				// Button.EnableInClassList("button-s", false);
			}
			public override void SelectState() {
				// parent.Settings(value);
				// Button.EnableInClassList("button-s", true);
			}

			private void MouseDownEvent(MouseDownEvent evt) => parent.Settings(element, value);
		}
	}
}
