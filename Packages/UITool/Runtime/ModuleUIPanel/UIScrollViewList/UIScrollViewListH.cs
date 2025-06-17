using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动列表 - 水平
	/// </summary>
	public class UIScrollViewListH<T, Data> : UIScrollViewH where T : ModuleUIItem<Data> {

		private ModuleUIItems<T, Data> Items;// UI项容器

		public UIScrollViewListH(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset,
		Func<Data, VisualElement, T> generate, UIDirection direction = UIDirection.FromLeftToRight) : base(element, canvas, direction) {
			Items = new ModuleUIItems<T, Data>(Container, templateAsset, generate);
		}
		public override void Dispose() {
			base.Dispose();
			Items.Dispose();
		}
		/// <summary> 释放资源 </summary>
		public void Release() => Items.Dispose();
		/// <summary> 创建UI项 </summary>
		public void Create(List<Data> datas) => Items.Create(datas);
		/// <summary> 创建UI项 </summary>
		public void Create(Data data) => Items.Create(data);
	}
}