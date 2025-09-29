using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 装备 - 编辑窗口
	/// </summary>
	public class EquipmentWindow : ModuleUIEditorWindow {

		public VisualTreeAsset ButtonTemplate = default;

		private EquipmentType currentEquipmentType;
		private UIEquipmentEnum<EquipmentType> equipmentType;
		private UIEquipmentEnum<WeaponType> weaponType;
		private UIEquipmentEnum<ArmorType> armorType;
		private UIEquipmentEnum<AccessoryType> accessoryType;
		private UIEquipmentList equipmentList;

		public VisualElement VEEquipmentType => Q<VisualElement>("EquipmentType");
		public VisualElement VEEquipmentExtendType => Q<VisualElement>("EquipmentExtendType");
		public VisualElement EquipmentList => Q<VisualElement>("EquipmentList");

		[MenuItem("Window/UI Toolkit/EquipmentWindow")]
		public static void ShowWindow() {
			EquipmentWindow wnd = GetWindow<EquipmentWindow>();
			wnd.titleContent = new GUIContent("EquipmentWindow");
		}

		public override void Initial() {
			equipmentType = new UIEquipmentEnum<EquipmentType>(VEEquipmentType, ButtonTemplate, SettingsEquipmentType);
			weaponType = new UIEquipmentEnum<WeaponType>(VEEquipmentExtendType, ButtonTemplate, (value) => SettingsEquipmentType(value.ToString()));
			armorType = new UIEquipmentEnum<ArmorType>(VEEquipmentExtendType, ButtonTemplate, (value) => SettingsEquipmentType(value.ToString()));
			accessoryType = new UIEquipmentEnum<AccessoryType>(VEEquipmentExtendType, ButtonTemplate, (value) => SettingsEquipmentType(value.ToString()));
			equipmentList = new UIEquipmentList(EquipmentList, rootVisualElement, ButtonTemplate, (value) => Debug.Log(value));
			AddControl(equipmentList);

			equipmentType.Initial();
		}
		public override void OnDestroy() {
			base.OnDestroy();
			equipmentType.Release();
			weaponType.Release();
			armorType.Release();
			accessoryType.Release();
		}

		/// <summary> 设置装备类型 </summary> 
		private void SettingsEquipmentType(EquipmentType type) {
			currentEquipmentType = type;
			if (type == EquipmentType.武器) { weaponType.Initial(); }
			if (type == EquipmentType.护甲) { armorType.Initial(); }
			if (type == EquipmentType.饰品) { accessoryType.Initial(); }
		}
		/// <summary> 设置装备类型 </summary>
		private void SettingsEquipmentType(string type) {
			string path = $"Assets/AssetsEquipment/ScriptableObject/{currentEquipmentType}/{type}";
			EnsureDirectoryExists(path);
			List<EquipmentConst> equipments = FindConstAssets<EquipmentConst>(path);
			equipmentList.Initial(equipments);
		}

		#region 编辑器工具
		/// <summary> 查找指定文件夹下的所有 T 类型的 ScriptableObject 资源 </summary> 
		public static List<T> FindConstAssets<T>(string folderPath) where T : ScriptableObject {
			List<T> result = new List<T>();
			string filter = $"t:{typeof(T).Name}";
			string[] guids = AssetDatabase.FindAssets(filter, new[] { folderPath });
			foreach (string guid in guids) {
				string assetPath = AssetDatabase.GUIDToAssetPath(guid);
				T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
				if (asset != null) { result.Add(asset); }
			}
			return result;
		}
		/// <summary> 确保目录存在 </summary>
		public static void EnsureDirectoryExists(string path) {
			if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
		}
		#endregion

		private void CreateConstWeapon() {
			// 创建一个新的 ConstWeapon 资源
			var asset = ScriptableObject.CreateInstance<WeaponConst>();
			string path = EditorUtility.SaveFilePanelInProject(
				"保存 ConstWeapon",
				"NewConstWeapon.asset",
				"asset",
				"请选择保存 ConstWeapon 的路径"
			);
			if (!string.IsNullOrEmpty(path)) {
				AssetDatabase.CreateAsset(asset, path);
				AssetDatabase.SaveAssets();
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = asset;
			}
		}
	}
}