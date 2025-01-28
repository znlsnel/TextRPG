using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
	public class InventoryScene : Scene
	{
		public InventoryScene(string name) : base(name)
		{
		}

		public override void StartScene()
		{
			Console.Clear();
			Console.WriteLine("[아이템 목록]");
			Console.WriteLine("1. 장착 관리");
			Console.WriteLine("0. 나가기");

			int value = GameManager.Instance.GetPlayerInputInt(0, 1);

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
				Console.WriteLine($"- {++cnt}. {(isEquip ? "[E] " : " ")}{items[i].GetItemInfo()}");

			}


			if (cnt == 0)
				Console.WriteLine("아이템이 없습니다.");

			Console.WriteLine();
			Console.WriteLine("0. 나가기"); 

			int idx = GameManager.Instance.GetPlayerInputInt(0, cnt);
			if (idx == 0) 
				return;

			idx -= 1;

			EquipItem(items[idx]);

		}

		void EquipItem(Item item)
		{
			Console.Clear();

			if (DataManager.Instance.inventory.EquipItem(item))
				Console.WriteLine($"{item.name}을 장착했습니다!");
			else
				Console.WriteLine($"{item.name}을 장착 해제 했습니다!");

			Console.WriteLine();
			Console.WriteLine("0. 돌아가기");

			int value = GameManager.Instance.GetPlayerInputInt(0, 0);
			if (value == 0)
				ShowInventory();
		}
	}
} 
