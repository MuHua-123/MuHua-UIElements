using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动列表 - 垂直
	/// </summary>
	public class UIScrollViewListV<T, Data> : UIScrollViewV where T : ModuleUIItem<Data> {
		private ModuleUIItems<T, Data> Items;// UI项容器

		public UIScrollViewListV(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset,
		Func<Data, VisualElement, T> generate, UIDirection direction = UIDirection.FromTopToBottom) : base(element, canvas, direction) {
			Items = new ModuleUIItems<T, Data>(Container, templateAsset, generate);
		}
		/// <summary> 释放资源 </summary>
		public void Release() => Items.Dispose();
		/// <summary> 创建UI项 </summary>
		public void Create(List<Data> datas) => Items.Create(datas);
		/// <summary> 创建UI项 </summary>
		public void Create(Data data) => Items.Create(data);
	}
}