using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;
using System;
using System.Security.AccessControl;

namespace MuHuaEditor {
	/// <summary>
	/// 装备 - 编辑窗口
	/// </summary>
	public class EquipmentWindow : ModuleUIEditorWindow {

		public VisualTreeAsset ButtonTemplate = default;

		private string type;
		private EquipmentConst equipment;
		private EquipmentType currentEquipmentType;

		private UIEquipmentEnum<EquipmentType> equipmentType;
		private UIEquipmentEnum<WeaponType> weaponType;
		private UIEquipmentEnum<ArmorType> armorType;
		private UIEquipmentEnum<AccessoryType> accessoryType;
		private UIEquipmentList equipmentList;
		private UIEquipmentSettings equipmentSettings;
		private UIEquipmentPanel equipmentPanel;

		public VisualElement VEEquipmentType => Q<VisualElement>("EquipmentType");
		public VisualElement VEEquipmentExtendType => Q<VisualElement>("EquipmentExtendType");
		public VisualElement EquipmentList => Q<VisualElement>("EquipmentList");
		public VisualElement EquipmentSettings => Q<VisualElement>("EquipmentSettings");
		public VisualElement EquipmentPanel => Q<VisualElement>("EquipmentPanel");
		public Button Button1 => Q<Button>("Button1");// 创建
		public Button Button2 => Q<Button>("Button2");// 删除
		public Button Button3 => Q<Button>("Button3");// 刷新

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
			equipmentList = new UIEquipmentList(EquipmentList, rootVisualElement, ButtonTemplate, Settings);
			AddControl(equipmentList);

			equipmentSettings = new UIEquipmentSettings(EquipmentSettings);
			equipmentPanel = new UIEquipmentPanel(EquipmentPanel);

			equipmentType.Initial();
			Button1.clicked += CreateEquipment;
			Button2.clicked += DeleteEquipment;
			Button3.clicked += () => Settings(equipment);
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
		/// <summary> 设置装备分类 </summary>
		private void SettingsEquipmentType(string type) {
			this.type = type;
			string path = $"Assets/AssetsEquipment/ScriptableObject/{currentEquipmentType}/{type}";
			EnsureDirectoryExists(path);
			List<EquipmentConst> equipments = FindConstAssets<EquipmentConst>(path);
			equipmentList.Initial(equipments);
		}
		/// <summary> 设置装备 </summary>
		private void Settings(EquipmentConst equipment) {
			this.equipment = equipment;
			Button2.EnableInClassList("ew-hide", equipment == null);
			equipmentSettings.Initial(equipment);
			equipmentPanel.Initial(equipment);
		}

		/// <summary> 创建装备 </summary>
		private void CreateEquipment() {
			// 创建一个新的 ConstWeapon 资源
			ScriptableObject asset = null;
			if (currentEquipmentType == EquipmentType.武器) {
				WeaponConst weapon = ScriptableObject.CreateInstance<WeaponConst>();
				weapon.type = (WeaponType)Enum.Parse(typeof(WeaponType), type);
				asset = weapon;
			}
			if (currentEquipmentType == EquipmentType.护甲) {
				ArmorConst armor = ScriptableObject.CreateInstance<ArmorConst>();
				armor.type = (ArmorType)Enum.Parse(typeof(ArmorType), type);
				asset = armor;
			}
			if (currentEquipmentType == EquipmentType.饰品) {
				AccessoryConst accessory = ScriptableObject.CreateInstance<AccessoryConst>();
				accessory.type = (AccessoryType)Enum.Parse(typeof(AccessoryType), type);
				asset = accessory;
			}

			string directory = $"Assets/AssetsEquipment/ScriptableObject/{currentEquipmentType}/{type}";
			EnsureDirectoryExists(directory);
			string path = GetUniqueAssetPath(directory, "Equipment");
			AssetDatabase.CreateAsset(asset, path);
			AssetDatabase.SaveAssets();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
			// 刷新装备列表
			SettingsEquipmentType(type);
		}
		/// <summary> 删除装备 </summary>
		private void DeleteEquipment() {
			if (equipment == null) { return; }
			string path = AssetDatabase.GetAssetPath(equipment);
			if (string.IsNullOrEmpty(path)) { return; }
			AssetDatabase.DeleteAsset(path);
			AssetDatabase.SaveAssets();
			equipment = null;
			// 刷新装备列表
			SettingsEquipmentType(type);
		}

		private string GetUniqueAssetPath(string directory, string baseName = "Equipment") {
			string path = $"{directory}/{baseName}.asset";
			int index = 1;
			while (File.Exists(path)) {
				path = $"{directory}/{baseName} {index}.asset";
				index++;
			}
			return path;
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
	}
}