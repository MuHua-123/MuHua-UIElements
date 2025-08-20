using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动列表 - 水平
	/// </summary>
	public class UIScrollViewListH<TItem, Data> : UIScrollViewH where TItem : ModuleUIItem<Data> {
		/// <summary> UI项容器 </summary>
		public ModuleUIItems<TItem, Data> Items;

		/// <summary> 索引器 </summary>
		public TItem this[int index] { get => Items[index]; }
		/// <summary> 计数 </summary>
		public int Count { get => Items != null ? Items.Count : 0; }

		public UIScrollViewListH(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset,
		Func<Data, VisualElement, TItem> generate, UIDirection direction = UIDirection.FromLeftToRight) : base(element, canvas, direction) {
			Items = new ModuleUIItems<TItem, Data>(Container, templateAsset, generate);
		}
		public override void Dispose() {
			base.Dispose();
			Items.Dispose();
		}
		/// <summary> 释放资源 </summary>
		public virtual void Release() => Items.Dispose();
		/// <summary> 创建UI项 </summary>
		public virtual void Create(List<Data> datas, bool isClear = true) => Items.Create(datas, isClear);
		/// <summary> 创建UI项 </summary>
		public virtual void Create(Data data, bool isClear = false) => Items.Create(data, isClear);
		/// <summary> 遍历 </summary>
		public virtual void ForEach(Action<TItem> action) => Items.ForEach(action);
		/// <summary> 选择第一个 </summary>
		public virtual void SelectFirst() => Items.SelectFirst();
		/// <summary> 选择最后一个 </summary>
		public virtual void SelectFinally() => Items.SelectFinally();
	}
}