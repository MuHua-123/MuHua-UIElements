using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// UI项容器
	/// </summary>
	public class ModuleUIItems<TItem, TData> : ModuleUIPanel where TItem : ModuleUIItem<TData> {
		/// <summary> 模板资源 </summary>
		public readonly VisualTreeAsset templateAsset;
		/// <summary> 生成UI项的函数 </summary>
		public readonly Func<TData, VisualElement, TItem> generate;
		/// <summary> UI项列表 </summary>
		public List<TItem> Items = new List<TItem>();

		/// <summary> 索引器 </summary>
		public TItem this[int index] { get => Items[index]; }
		/// <summary> 计数 </summary>
		public int Count { get => Items != null ? Items.Count : 0; }

		/// <summary>  UI容器  </summary>
		public ModuleUIItems(VisualElement element, VisualTreeAsset templateAsset, Func<TData, VisualElement, TItem> generate) : base(element) {
			this.templateAsset = templateAsset;
			this.generate = generate;
		}
		/// <summary> 释放资源 </summary>
		public void Release() {
			element.Clear();
			Items.ForEach(obj => obj.Dispose());
			Items = new List<TItem>();
		}
		/// <summary> 创建UI项 </summary>
		public void Create(List<TData> datas, bool isClear = true) {
			if (isClear) { Release(); }
			datas.ForEach(Create);
		}
		/// <summary> 创建UI项 </summary>
		public void Create(TData data, bool isClear = false) {
			if (isClear) { Release(); }
			Create(data);
		}
		/// <summary> 创建UI项 </summary>
		public void Create(TData data) {
			VisualElement element = templateAsset.Instantiate();
			TItem item = generate(data, element);
			this.element.Add(item.element);
			Items.Add(item);
		}
		/// <summary> 遍历 </summary>
		public void ForEach(Action<TItem> action) {
			Items.ForEach(action);
		}
		/// <summary> 选择第一个 </summary>
		public void SelectFirst() {
			if (Items.Count > 0) { Items[0].Select(); }
		}
		/// <summary> 选择最后一个 </summary>
		public void SelectFinally() {
			if (Items.Count > 0) { Items[Count - 1].Select(); }
		}
	}
}
