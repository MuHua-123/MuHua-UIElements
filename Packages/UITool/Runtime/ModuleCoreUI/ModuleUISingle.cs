using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 页面单例
	/// </summary>
	public abstract class ModuleUISingle<Single> : MonoBehaviour where Single : ModuleUISingle<Single> {
		/// <summary> 模块单例 </summary>
		public static Single I => instance;
		/// <summary> 模块单例 </summary>
		protected static Single instance;
		/// <summary> 初始化 </summary>
		protected abstract void Awake();

		/// <summary> 绑定文档 </summary>
		public UIDocument document;
		/// <summary> 根目录文档 </summary>
		public VisualElement root => document.rootVisualElement;
		/// <summary> 绑定文档 </summary>
		public abstract VisualElement Element { get; }
		/// <summary> 查询UI元素 </summary>
		public T Q<T>(string name = null, string className = null) where T : VisualElement => Element.Q<T>(name, className);

		/// <summary> 替换，并且设置切换场景不销毁 </summary>
		protected virtual void Replace(bool isDontDestroy = true) {
			if (instance != null) { Destroy(instance.gameObject); }
			instance = (Single)this;
			if (isDontDestroy) { DontDestroyOnLoad(gameObject); }
		}
		/// <summary> 不替换，并且设置切换场景不销毁 </summary>
		protected virtual void NoReplace(bool isDontDestroy = true) {
			if (isDontDestroy) { DontDestroyOnLoad(gameObject); }
			if (instance == null) { instance = (Single)this; }
			else { Destroy(gameObject); }
		}
	}
}