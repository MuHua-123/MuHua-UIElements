using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace MuHuaEditor {
	/// <summary>
	/// 数值容器 - 自定义编辑器
	/// </summary>
	[CustomEditor(typeof(ValueContainerConst))]
	public class ValueContainerConstEditor : ModuleUIEditor<ValueContainerConst> {

		public VisualTreeAsset ValueTemplate = default;

		private UIValueContainerConst valueContainer;

		public VisualElement ValueList => Q<VisualElement>("ValueList");
		public Button Button1 => Q<Button>("Button1");// 创建属性
		public Button Button2 => Q<Button>("Button2");// 删除全部

		public override void Initial() {
			valueContainer = new UIValueContainerConst(ValueList, ValueTemplate);
			valueContainer.Initial(value);
			AddControl(valueContainer);

			Button1.clicked += GenerateAttribute;
			Button2.clicked += DeleteInstance;
		}

		/// <summary> 创建属性实例 </summary>
		private void GenerateAttribute() {
			ValueInstanceConst instance = CreateInstance<ValueInstanceConst>();
			instance.container = value;
			// 加入容器
			value.instances.Add(instance);
			AssetDatabase.AddObjectToAsset(instance, value);
			EditorUtility.SetDirty(instance);
			EditorUtility.SetDirty(value);
			AssetDatabase.SaveAssets();
		}
		/// <summary> 删除全部实例 </summary>
		private void DeleteInstance() {
			for (int i = value.instances.Count; i-- > 0;) {
				ValueInstanceConst instance = value.instances[i];
				value.instances.Remove(instance);
				Undo.DestroyObjectImmediate(instance);
			}
			EditorUtility.SetDirty(value);
			AssetDatabase.SaveAssets();
		}
	}
}
