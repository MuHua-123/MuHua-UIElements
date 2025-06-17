using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	public class UILongField : LongField {
		public new class UxmlFactory : UxmlFactory<UILongField, UxmlTraits> { }
		public new class UxmlTraits : LongField.UxmlTraits { }
		public UILongField() {
			ClearClassList();
			AddToClassList("input-field");

			labelElement.ClearClassList();
			labelElement.AddToClassList("unity-text-element");
			labelElement.AddToClassList("input-field-label");

			VisualElement inputElement = this.Q<VisualElement>("unity-text-input");
			inputElement.ClearClassList();
			inputElement.AddToClassList("input-field-box");

			VisualElement textElement = inputElement.Q<VisualElement>("");
			textElement.ClearClassList();
			textElement.AddToClassList("unity-text-element");
			textElement.AddToClassList("input-field-text");
		}
	}
}
