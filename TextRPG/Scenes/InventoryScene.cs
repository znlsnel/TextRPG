using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InventoryScene : Scene
{
	public InventoryScene(string name) : base(name){} 

	public override void EnterScene()
	{
		Console.Clear();
		Console.WriteLine("[아이템 목록]");
		Console.WriteLine("1. 장착 관리");
		Console.WriteLine("0. 나가기");

		int value = SpartaRPG.SelectOption(0, 1);

		if (value == 0)
			return;

		if (value == 1)
			ShowInventory();
	} 
		  
	public void ShowInventory()
	{
		Console.Clear(); 
		List<Item> items = DataManager.Instance.inventory.GetPlayerItem();
		Console.WriteLine("[아이템 목록]");
		
		int cnt = 0;
		for (int i = 0; i < items.Count; i++)
		{
			bool isEquip = DataManager.Instance.inventory.IsEquippedItem(items[i]);
			Console.ForegroundColor = isEquip ?  ConsoleColor.Green : ConsoleColor.White;
			Console.WriteLine($"- {++cnt}.{(isEquip ? " [E]" : " ")}{items[i].GetItemInfo()}");

		}
		Console.ForegroundColor =  ConsoleColor.White;

		if (cnt == 0)
			Console.WriteLine("아이템이 없습니다.");

		Console.WriteLine();
		Console.WriteLine("0. 나가기"); 

		int idx = SpartaRPG.SelectOption(0, cnt);
		if (idx == 0) 
			return;

		idx -= 1;

		EquipItem(items[idx]);

	}
	 
	void EquipItem(Item item)
	{
		Console.Clear();

		// 직업에 맞지 않는다면 장착 실패 처리
		if (item.CanEquip(DataManager.Instance.playerData.classType) == false)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"착용이 불가능합니다! 해당 아이템은 [{DataManager.Instance.classNames[item.equipableBy]}]전용 아이템입니다.");
			Console.ForegroundColor = ConsoleColor.White;  
		}
		else if (DataManager.Instance.inventory.EquipItem(item))
			Console.WriteLine($"{item.name}을 장착했습니다!");
		else
			Console.WriteLine($"{item.name}을 장착 해제 했습니다!");

		Console.WriteLine();
		Console.WriteLine("0. 돌아가기");

		int value = SpartaRPG.SelectOption(0, 0);
		if (value == 0)
			ShowInventory();
	}
}

