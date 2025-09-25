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

		WeaponSlot weaponSlot = new WeaponSlot(SlotType.主手);
		DeputySlot deputySlot = new DeputySlot(SlotType.副手);
		weaponSlot.deputy = deputySlot;
		deputySlot.weapon = weaponSlot;

		equipment.AddSlot(weaponSlot);
		equipment.AddSlot(deputySlot);

		equipment.AddSlot(new ArmorSlot(SlotType.上衣, ArmorType.上衣));
		equipment.AddSlot(new ArmorSlot(SlotType.头盔, ArmorType.头盔));
		equipment.AddSlot(new ArmorSlot(SlotType.手套, ArmorType.手套));
		equipment.AddSlot(new ArmorSlot(SlotType.腰带, ArmorType.腰带));
		equipment.AddSlot(new ArmorSlot(SlotType.鞋子, ArmorType.鞋子));

		equipment.AddSlot(new AccessorySlot(SlotType.项链, AccessoryType.项链));
		equipment.AddSlot(new AccessorySlot(SlotType.戒指1, AccessoryType.戒指));
		equipment.AddSlot(new AccessorySlot(SlotType.戒指2, AccessoryType.戒指));
		equipment.AddSlot(new AccessorySlot(SlotType.手镯1, AccessoryType.手镯));
		equipment.AddSlot(new AccessorySlot(SlotType.手镯2, AccessoryType.手镯));

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
