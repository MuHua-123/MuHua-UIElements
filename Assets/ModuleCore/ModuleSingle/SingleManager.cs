using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 全局管理器
/// </summary>
public class SingleManager : ModuleSingle<SingleManager> {

	public Inventory inventory;
	public Equipment equipment;

	public ItemReward materialReward;
	public ItemReward equipmentReward;

	protected override void Awake() => NoReplace();

	private void Start() {
		inventory = new Inventory(40);
		equipment = new Equipment();

		WeaponSlot weaponSlot = new WeaponSlot("主手");
		DeputySlot deputySlot = new DeputySlot("副手");
		weaponSlot.deputy = deputySlot;
		deputySlot.weapon = weaponSlot;

		equipment.AddSlot(weaponSlot);
		equipment.AddSlot(deputySlot);

		equipment.AddSlot(new ArmorSlot("上衣"));
		equipment.AddSlot(new ArmorSlot("头盔"));
		equipment.AddSlot(new ArmorSlot("手套"));
		equipment.AddSlot(new ArmorSlot("腰带"));
		equipment.AddSlot(new ArmorSlot("鞋子"));

		equipment.AddSlot(new AccessorySlot("项链"));
		equipment.AddSlot(new AccessorySlot("戒指1"));
		equipment.AddSlot(new AccessorySlot("戒指2"));
		equipment.AddSlot(new AccessorySlot("手镯1"));
		equipment.AddSlot(new AccessorySlot("手镯2"));

		materialReward = new ItemReward();
		materialReward.Settings(ManagerItem.I.materials);
		equipmentReward = new ItemReward();
		equipmentReward.Settings(ManagerItem.I.equipments);

		UIShortcutMenu shortcutMenu = UIPopupManager.I.shortcutMenu;
		shortcutMenu.Add("背包", () => { ModuleUI.Settings(Page.Backpack); });
		shortcutMenu.Add("奖励/材料", RewardMaterial);
		shortcutMenu.Add("奖励/装备", RewardEquipment);
	}


	private void RewardMaterial() {
		(DataItem item, int count) = materialReward.Get(10);
		inventory.AddItem(item, count);
	}
	private void RewardEquipment() {
		(DataItem item, int count) = equipmentReward.Get(1);
		inventory.AddItem(item, count);
	}
}
