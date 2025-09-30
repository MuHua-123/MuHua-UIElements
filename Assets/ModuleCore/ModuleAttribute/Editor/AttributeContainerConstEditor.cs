using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace MuHuaEditor {
	/// <summary>
	/// 属性容器 - 自定义编辑器
	/// </summary>
	[CustomEditor(typeof(AttributeContainerConst))]
	public class AttributeContainerConstEditor : ModuleUIEditor<AttributeContainerConst> {

		public VisualTreeAsset AttributeTemplate = default;

		private UIAttributeList attributeList;

		public VisualElement AttributeList => Q<VisualElement>("AttributeList");
		public Button Button1 => Q<Button>("Button1");// 创建属性
		public Button Button2 => Q<Button>("Button2");// 删除全部

		public override void Initial() {
			attributeList = new UIAttributeList(AttributeList, AttributeTemplate);
			attributeList.Initial(value);
			AddControl(attributeList);

			Button1.clicked += GenerateAttribute;
			Button2.clicked += DeleteInstance;
		}

		/// <summary> 创建属性实例 </summary>
		private void GenerateAttribute() {
			AttributeInstanceConst instance = CreateInstance<AttributeInstanceConst>();
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
				AttributeInstanceConst instance = value.instances[i];
				value.instances.Remove(instance);
				Undo.DestroyObjectImmediate(instance);
			}
			EditorUtility.SetDirty(value);
			AssetDatabase.SaveAssets();
		}
	}
}