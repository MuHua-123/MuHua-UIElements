using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

#if ENABLE_INPUT_SYSTEM && UNITY_INPUT_SYSTEM_PACKAGE
using UnityEngine.InputSystem;
#endif

namespace MuHua {
	/// <summary>
	/// UI工具
	/// </summary>
	public static class UITool {
		/// <summary> 获取鼠标位置 </summary>
		public static Vector3 GetMousePosition() {
#if ENABLE_INPUT_SYSTEM && UNITY_INPUT_SYSTEM_PACKAGE
            return Mouse.current.position.ReadValue();
#else
			return Input.mousePosition;
#endif
		}

		/// <summary> 获取鼠标位置(元素中的鼠标位置) </summary>
		public static Vector3 GetMousePosition(VisualElement element) {
			Vector3 mousePosition = GetMousePosition();
			float offsetX = mousePosition.x / Screen.width;
			float offsetY = mousePosition.y / Screen.height;

			float x = element.resolvedStyle.width * offsetX;
			float y = element.resolvedStyle.height * (1 - offsetY);

			return new Vector3(x, y);
		}

		/// <summary> 富文本: 颜色 </summary>
		public static string RichTextColor(string value, Color color) {
			string hexRGBA = ColorUtility.ToHtmlStringRGBA(color);
			return $"<color=#{hexRGBA}>{value}</color>";
		}
		/// <summary> 富文本: 下划线 </summary>
		public static string RichTextUnderline(string value) {
			return $"<u>{value}</u>";
		}
		/// <summary> 排除富文本 </summary>
		public static string RemoveRichText(string str) {
			return Regex.Replace(str, "<.*?>", "");
		}
		/// <summary> 判断 localPosition 是否在 string 上 </summary>
		public static bool EnterString(this Label label, string str, Vector2 localPosition) {
			string rawText = RemoveRichText(label.text);
			int startIndex = rawText.IndexOf(str);
			if (startIndex == -1) return false;

			// 计算起始位置宽度
			float startWidth = label.MeasureTextSize(
				rawText.Substring(0, startIndex),
				label.resolvedStyle.width,
				VisualElement.MeasureMode.Undefined,
				label.resolvedStyle.height,
				VisualElement.MeasureMode.Undefined
			).x;

			// 计算目标字符串总宽度
			float targetWidth = label.MeasureTextSize(
				str,
				label.resolvedStyle.width,
				VisualElement.MeasureMode.Undefined,
				label.resolvedStyle.height,
				VisualElement.MeasureMode.Undefined
			).x;

			Rect rect = new Rect(startWidth, 0, targetWidth, label.resolvedStyle.height);
			return rect.Contains(localPosition);
		}
	}
}