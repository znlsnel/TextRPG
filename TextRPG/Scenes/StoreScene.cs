using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class StoreScene : Scene
{
	DataManager data;
	public StoreScene(string name) : base(name)
	{
		data = DataManager.Instance;
	}
	int sellPriceRate = 85;
	public override void StartScene()
	{
		Console.Clear();
		Console.WriteLine("[상점]");
		Console.WriteLine();
		Console.WriteLine("[보유 골드]");

		PlayerData pd = data.playerData;
		Console.WriteLine(pd.gold + " G");

		Dictionary<string, Item> items = data.items;

		Console.WriteLine();

		foreach (var item in items)
		{
			bool isOwned = data.inventory.isOwnedItem(item.Value);
			Console.ForegroundColor = isOwned ? ConsoleColor.Green : ConsoleColor.White;
			Console.WriteLine($"- {item.Value.GetItemInfo()} | {(isOwned ? "구매완료" : $"{item.Value.price} G")}");
		}
		Console.ForegroundColor = ConsoleColor.White;

		Console.WriteLine("\n1. 아이템 구매");
		Console.WriteLine("2. 아이템 판매");
		Console.WriteLine("0. 나가기");

		int value = SpartaRPG.SelectOption(0, 2);

		if (value == 1)
			OpenItemInfo();
		else if (value == 2)
			OpenSellItemInfo();

	}

	public void OpenItemInfo()
	{
		Console.Clear();
		Console.WriteLine("[상점 - 아이템 구매]");
		Console.WriteLine("\n[보유 골드]");

		PlayerData pd = data.playerData;
		Console.WriteLine(pd.gold + " G");

		Dictionary<string, Item> temp = data.items;
		List<Item> items = new List<Item>();
		foreach (var i in temp)
			items.Add(i.Value);

		Console.WriteLine();

		int cnt = 0;
		foreach (var item in items)
		{
			bool isOwned = data.inventory.isOwnedItem(item);
			Console.ForegroundColor = isOwned ? ConsoleColor.Green : ConsoleColor.White;
			Console.WriteLine($"- {++cnt} {item.GetItemInfo()} | {(isOwned ? "구매완료" : $"{item.price} G")}");
		}
		Console.ForegroundColor = ConsoleColor.White;

		Console.WriteLine("\n0. 나가기");

		int value = SpartaRPG.SelectOption(0, cnt);
		if (value == 0)
		{
			StartScene();
			return;

		}

		BuyItem(items[value - 1]); 
		OpenItemInfo();
	}

	void BuyItem(Item item)
	{
		Console.Clear();
		bool hasEnoughGold = item.price > DataManager.Instance.playerData.gold;
		bool bought = data.inventory.isOwnedItem(item);

		if (bought || hasEnoughGold)
		{
			if (bought)
				Console.WriteLine("이미 구매한 아이템입니다.");
			else
				Console.WriteLine("골드가 부족합니다.");

				
		}
		else
		{
			data.playerData.gold -= item.price;
			data.inventory.ownedItems.Add(item.name);
			Console.WriteLine($"{item.name}을 구매했습니다!");
		}
			
		Console.WriteLine();
		Console.WriteLine("0. 돌아가기");
		SpartaRPG.SelectOption(0, 0);
	}

	void OpenSellItemInfo()
	{
		Console.Clear();
		Console.WriteLine("[상점 - 아이템 판매]");
		Console.WriteLine("보유중인 아이템을 판매할 수 있습니다.");

		Console.WriteLine();
		Console.WriteLine("[보유 골드]");

		PlayerData pd = data.playerData;
		Console.WriteLine($"{pd.gold} G"); 

		Console.WriteLine();
		Console.WriteLine("[아이템 목록]");

		List<Item> items = data.inventory.GetPlayerItem();


		int cnt = 0; 
		foreach (Item item in items)
			Console.WriteLine($"- {++cnt} {item.GetItemInfo()} | {item.price * sellPriceRate / 100} G");


		Console.WriteLine("\n0. 나가기");

		int idx = SpartaRPG.SelectOption(0, cnt);

		if (idx-- == 0)
		{
			StartScene();
			return;
		}

		SellItem(items[idx]);
		OpenSellItemInfo();
	}

	void SellItem(Item item)
	{
		int price = item.price * sellPriceRate / 100;
		data.playerData.gold += price;
			 
		data.inventory.RemoveItem(item);

		Console.Clear();
		Console.WriteLine($"{item.name}을 판매했습니다! ( + {price} G )");

		Console.WriteLine("\n0. 돌아가기");
		SpartaRPG.SelectOption(0, 0);
	}
}

 