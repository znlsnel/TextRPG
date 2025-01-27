using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
	public class StoreScene : Scene
	{
		public StoreScene(string name) : base(name)
		{
		}

		public override void StartScene()
		{
			Console.Clear();
			Console.WriteLine("[상점]");
			Console.WriteLine();
			Console.WriteLine("[보유 골드]");

			PlayerData pd = DataManager.Instance.playerData;
			Console.WriteLine(pd.gold + " G");

			Dictionary<string, Item> items = DataManager.Instance.items;

			Console.WriteLine();

			foreach (var item in items)
			{
				Console.Write($"- {item.Value.GetItemInfo()}");
				if (DataManager.Instance.isOwnItem(item.Value))
					Console.Write(" 구매완료");
				else
					Console.Write($" {item.Value.price} G");
			Console.WriteLine();
			}

		    Console.WriteLine();
		    Console.WriteLine("1. 아이템 구매");
		    Console.WriteLine("0. 나가기");

			int value = GameManager.Instance.GetPlayerInputInt(0, 1);
			if (value == 0) 
				return;

			if (value == 1)
				OpenItemInfo();

		}

		public void OpenItemInfo()
		{
			Console.Clear();
			Console.WriteLine("[상점 - 아이템 구매]");
			Console.WriteLine();
			Console.WriteLine("[보유 골드]");

			PlayerData pd = DataManager.Instance.playerData;
			Console.WriteLine(pd.gold + " G");

			Dictionary<string, Item> temp = DataManager.Instance.items;
			List<Item> items = new List<Item>();
			foreach (var i in temp)
				items.Add(i.Value);

			Console.WriteLine();

			int cnt = 0;
			foreach (var item in items)
			{
				Console.Write($"- {++cnt} {item.GetItemInfo()}");
				if (DataManager.Instance.isOwnItem(item))
					Console.Write(" 구매완료");
				else
					Console.Write($" {item.price} G");
				Console.WriteLine();
			}

			Console.WriteLine();
			Console.WriteLine("0. 나가기");

			int value = GameManager.Instance.GetPlayerInputInt(0, cnt);
			if (value == 0)
				return;

			BuyItem(items[value - 1]);
		}


		public void BuyItem(Item item)
		{
			Console.Clear();
			bool hasEnoughGold = item.price > DataManager.Instance.playerData.gold;
			bool bought = DataManager.Instance.isOwnItem(item);

			if (bought || hasEnoughGold)
			{
				if (bought)
					Console.WriteLine("이미 구매한 아이템입니다.");
				else
					Console.WriteLine("골드가 부족합니다.");

				
			}
			else
			{
				DataManager.Instance.playerData.gold -= item.price;
				DataManager.Instance.inventoryData.ownedItems.Add(item.name);
				Console.WriteLine($"{item.name}을 구매했습니다!");
			}
			
			Console.WriteLine();
			Console.WriteLine("0. 돌아가기");
			int value = GameManager.Instance.GetPlayerInputInt(0, 0);

			if (value == 0)
				OpenItemInfo();
		}
	}
}
