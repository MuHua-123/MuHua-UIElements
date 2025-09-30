using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 属性实例 - 自定义编辑器
	/// </summary>
	[CustomEditor(typeof(AttributeInstanceConst))]
	public class AttributeInstanceConstEditor : ModuleUIEditor<AttributeInstanceConst> {

		public UIAttributePanel attributePanel;

		public VisualElement AttributePanel => Q<VisualElement>("AttributePanel");
		public FloatField Min => Q<FloatField>("Min");
		public FloatField Max => Q<FloatField>("Max");
		public Button button => Q<Button>("Button");

		public override void Initial() {
			attributePanel = new UIAttributePanel(value, AttributePanel);

			Min.SetValueWithoutNotify(value.minValue);
			Max.SetValueWithoutNotify(value.maxValue);

			Min.RegisterCallback<FocusOutEvent>(evt => ModifyMin());
			Max.RegisterCallback<FocusOutEvent>(evt => ModifyMax());

			button.clicked += DeleteInstance;
		}

		/// <summary> 修改最小值 </summary>
		private void ModifyMin() { value.minValue = Min.value; Dirty(); }
		/// <summary> 修改最大值 </summary>
		private void ModifyMax() { value.maxValue = Max.value; Dirty(); }

		/// <summary> 保存资源 </summary>
		private void Dirty() {
			EditorUtility.SetDirty(value);
			AssetDatabase.SaveAssets();
		}

		/// <summary> 删除实例 </summary>
		private void DeleteInstance() {
			AttributeContainerConst container = value.container;
			container.instances.Remove(value);
			EditorUtility.SetDirty(container);
			Undo.DestroyObjectImmediate(value);
			AssetDatabase.SaveAssets();
		}
	}
}
