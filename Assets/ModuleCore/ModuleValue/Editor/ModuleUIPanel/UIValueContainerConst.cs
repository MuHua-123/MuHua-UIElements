using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using MuHua;
using PlasticGui;

namespace MuHuaEditor {
	/// <summary>
	/// 数值容器 - UI面板
	/// </summary>
	public class UIValueContainerConst : ModuleUIPanel, UIControl {
		/// <summary> 模板资源 </summary>
		public VisualTreeAsset templateAsset;
		/// <summary> 数值列表 </summary>
		public ValueContainerConst container;
		/// <summary> 项目列表 </summary>
		public ModuleUIItems<UIItem, ValueInstanceConst> items;
		/// <summary> 列表拖拽 </summary>
		public UIValueContainerConstDrag valueContainerConstDrag;

		public UIValueContainerConst(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
			this.templateAsset = templateAsset;
			items = new ModuleUIItems<UIItem, ValueInstanceConst>(element, templateAsset,
			(data, element) => new UIItem(data, element, this));

			valueContainerConstDrag = new UIValueContainerConstDrag(element, templateAsset);
		}

		public void Update() => valueContainerConstDrag.Update();

		public void Dispose() => items.Release();

		/// <summary> 初始化 </summary>
		public void Initial(ValueContainerConst container) {
			this.container = container;
			items.Create(container.instances);
			valueContainerConstDrag.Initial(container);
		}
		/// <summary> 设置值 </summary>
		public void Settings(VisualElement element, ValueInstanceConst value) {
			valueContainerConstDrag.Settings(element, value);
		}

		/// <summary> UI项 </summary>
		public class UIItem : ModuleUIItem<ValueInstanceConst> {

			public UIValueContainerConst parent;

			public Label Range => Q<Label>("Range");
			public TextField Name => Q<TextField>("Name");
			public EnumField Type => Q<EnumField>("Type");
			public FloatField Base => Q<FloatField>("Base");
			public Toggle Boolean => Q<Toggle>("Boolean");

			public UIItem(ValueInstanceConst value, VisualElement element) : base(value, element) {
				Initial();
			}
			public UIItem(ValueInstanceConst value, VisualElement element, UIValueContainerConst parent) : base(value, element) {
				this.parent = parent;
				Initial();
				element.RegisterCallback<MouseDownEvent>(MouseDownEvent);
			}
			private void Initial() {
				Name.SetValueWithoutNotify(value.name);
				Type.Init(value.type);
				Base.SetValueWithoutNotify(value.baseValue);
				Boolean.value = value.baseValue == value.maxValue;

				VisualElement temp = value.type == ValueType.Boolean ? Base : Boolean;
				temp.style.display = DisplayStyle.None;

				string symbol = value.type == ValueType.Percentage ? "%" : "";
				Range.text = $"{value.minValue}{symbol} ~ {value.maxValue}{symbol}";

				Name.RegisterCallback<FocusOutEvent>(evt => ModifyName());
				Type.RegisterValueChangedCallback(evt => ModifyType((ValueType)evt.newValue));
				Base.RegisterCallback<FocusOutEvent>(evt => ModifyValue());
				Boolean.RegisterValueChangedCallback(evt => ModifyBoolean());
			}
			private void MouseDownEvent(MouseDownEvent evt) => parent.Settings(element, value);

			/// <summary> 修改名字 </summary>
			private void ModifyName() { value.name = Name.value; Dirty(); }
			/// <summary> 修改类型 </summary>
			private void ModifyType(ValueType type) {
				if (type == ValueType.Float) { value.InitialFloat(); }
				if (type == ValueType.Integer) { value.InitialInteger(); }
				if (type == ValueType.Boolean) { value.InitialBoolean(); }
				if (type == ValueType.Percentage) { value.InitialPercentage(); }

				VisualElement temp = value.type == ValueType.Boolean ? Base : Boolean;
				temp.style.display = DisplayStyle.None;

				Dirty();
			}
			/// <summary> 修改默认值 </summary>
			private void ModifyValue() { value.baseValue = Base.value; Dirty(); }
			/// <summary> 修改布尔值 </summary>
			private void ModifyBoolean() { value.baseValue = Boolean.value ? value.maxValue : value.minValue; Dirty(); }

			/// <summary> 保存资源 </summary>
			private void Dirty() {
				EditorUtility.SetDirty(value);
				AssetDatabase.SaveAssets();
			}
		}
	}
	public class UIValueContainerConstDrag : ModuleUIPanel, UIControl {
		/// <summary> 模板资源 </summary>
		public VisualTreeAsset templateAsset;
		/// <summary> 偏移位置 </summary>
		public Vector2 mouseOffset;
		/// <summary> 鼠标偏移 </summary>
		public Vector2 mousePosition;
		/// <summary> 位置偏移 </summary>
		public Vector2 positionOffset;
		/// <summary> 移动目标 </summary>
		public VisualElement moving;
		/// <summary> 拖拽预览 </summary>
		public VisualElement preview;
		/// <summary> 项目预览 </summary>
		public UIValueContainerConst.UIItem previewItem;
		/// <summary> 数值列表 </summary>
		public ValueContainerConst container;

		public UIValueContainerConstDrag(VisualElement element, VisualTreeAsset templateAsset) : base(element) {
			this.templateAsset = templateAsset;
			element.RegisterCallback<MouseUpEvent>(MouseUpEvent);
			element.RegisterCallback<MouseLeaveEvent>(MouseUpEvent);
			element.RegisterCallback<MouseMoveEvent>(MouseMoveEvent);
		}
		private void MouseUpEvent(EventBase evt) {
			if (preview == null) { return; }
			preview.parent.Remove(preview);
			preview = null;
			moving.EnableInClassList("value-hide", false);
			if (previewItem != null) { previewItem.Dispose(); }

			EditorUtility.SetDirty(container);
			AssetDatabase.SaveAssets();
		}
		private void MouseMoveEvent(MouseMoveEvent evt) {
			mousePosition = evt.mousePosition;
		}

		public void Update() {
			if (preview == null) { return; }
			preview.transform.position = positionOffset + mousePosition - mouseOffset;
			int insertIndex = GetInsertIndex(element, preview);
			int oldIndex = element.IndexOf(moving);
			// 修正插入索引
			if (insertIndex > oldIndex) insertIndex--;
			if (oldIndex != insertIndex) { MovePreviewElement(oldIndex, insertIndex); }
		}

		public void Dispose() {
			// throw new System.NotImplementedException();
		}

		/// <summary> 初始化 </summary>
		public void Initial(ValueContainerConst container) {
			this.container = container;
		}
		/// <summary> 设置值 </summary>
		public void Settings(VisualElement moving, ValueInstanceConst value) {
			this.moving = moving;
			moving.EnableInClassList("value-hide", true);

			preview = templateAsset.Instantiate();
			previewItem = new UIValueContainerConst.UIItem(value, preview);
			element.Add(preview);

			preview.EnableInClassList("value-preview", true);
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

			ValueInstanceConst instanceConst = container.instances[oldIndex];
			container.instances.RemoveAt(oldIndex);
			container.instances.Insert(newIndex, instanceConst);
		}
	}
}
