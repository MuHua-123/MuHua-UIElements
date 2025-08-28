using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHua {
	/// <summary>
	/// 滑块按钮 - 数据
	/// </summary>
	public abstract class DataSlideButton {
		/// <summary> 绑定的元素 </summary>
		public VisualElement element;
	}
	/// <summary>
	/// 滑动按钮
	/// </summary>
	public class UISlideButton<TItem, Data> : ModuleUIPanel, UIControl where TItem : ModuleUIItem<Data> where Data : DataSlideButton {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;

		/// <summary> 选中数据 </summary>
		public Data data;
		/// <summary> UI项容器 </summary>
		public ModuleUIItems<TItem, Data> Items;

		/// <summary> 索引器 </summary>
		public TItem this[int index] { get => Items[index]; }
		/// <summary> 计数 </summary>
		public int Count { get => Items != null ? Items.Count : 0; }

		/// <summary> 滑块 </summary>
		public VisualElement Slide => Q<VisualElement>("Slide");
		/// <summary> 容器 </summary>
		public VisualElement Container => Q<VisualElement>("Container");

		public UISlideButton(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset, Func<Data, VisualElement, TItem> generate) : base(element) {
			this.canvas = canvas;

			Items = new ModuleUIItems<TItem, Data>(Container, templateAsset, generate);

			ModuleUIItem<Data>.OnSelect += Settings;
		}
		public virtual void Dispose() {
			Items.Release();
		}
		public virtual void Update() {
			if (data == null || data.element == null) { return; }
			Vector3 offset = data.element.worldBound.position - Slide.worldBound.position;
			Slide.transform.position += offset * Time.deltaTime * 20;
		}

		/// <summary> 设置UI项 </summary>
		public virtual void Settings(Data data) => this.data = data;
		/// <summary> 释放资源 </summary>
		public virtual void Release() => Items.Release();
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