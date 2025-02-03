using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public struct DungeonInfo
{
	public string name;
	public int level;
	public int reward;

	public DungeonInfo(string n, int l, int r)
	{
		name = n;
		level = l;
		reward = r;
	}
}
public class DungeonScene : Scene
{
	public DungeonScene(string name) : base(name) { }
	Random rand = new Random();

	//int[] levels = new int[4] { 5, 11, 17, 30};
	//int[] rewards = new int[4] { 1000, 1700, 2500, 10000 };
	//string[] names = new string[4] { "쉬움", "일반", "어려운", "죽음의" };
	List<DungeonInfo> dungeons = new List<DungeonInfo>() 
	{
		new DungeonInfo("쉬움", 5, 1000),
		new DungeonInfo("일반", 11, 2000),
		new DungeonInfo("어려운", 17, 3000),
		new DungeonInfo("죽음의", 30, 10000),
		new DungeonInfo("지옥의", 300, 100000),
	};

	public override void EnterScene()
	{
		Console.Clear();
		Console.WriteLine("[던전입장]");
		Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
		Console.WriteLine("");
		
		for (int i = 0; i < dungeons.Count; i++)
			Console.WriteLine($"{i+1}. {dungeons[i].name} 던전 \t 방어력 {dungeons[i].level} 이상 권장");
		
		Console.WriteLine("0. 나가기");

		int value = SpartaRPG.SelectOption(0, dungeons.Count);

		if (value != 0)
		{
			EnterDungeon(dungeons[value - 1]);
		}
	}

	public void EnterDungeon(DungeonInfo dungeon)
	{
		Console.Clear();
		PlayerData pd = DataManager.Instance.playerData;

		if (pd.hp == 0)
		{
			Console.WriteLine("[던전 실패..]");
			Console.WriteLine($"현재 체력이 0입니다. 회복 후에 다시 도전 해주세요.");

			Console.WriteLine();
			Console.WriteLine("0. 나가기");
			SpartaRPG.SelectOption(0, 0);
			EnterScene();
			return;
		}

		int damage = dungeon.level - pd.defense;
		int minusHp = Math.Max(rand.Next(0, 4), rand.Next(20, 36) + damage); 
		int rewardGold = 0;

		if (pd.hp <= minusHp || (pd.defense < dungeon.level && rand.Next(0, 100) < 40))
		{
			Console.WriteLine("[던전 클리어 실패..]");
			Console.WriteLine($"{dungeon.name} 던전 클리어에 실패 하셨습니다...");

			if (pd.hp > minusHp)
				minusHp = pd.maxHp / 2;
		}
		else
		{
			Console.WriteLine("[던전 클리어]");
			Console.WriteLine($"축하합니다!!");
			Console.WriteLine($"{dungeon.name} 던전을 클리어 하였습니다!");
			int add = (dungeon.reward * pd.attack / 100);
			rewardGold = dungeon.reward + rand.Next(add, add * 2);
		}

		Console.WriteLine("");
		Console.WriteLine("[탐험 결과]");

		Console.WriteLine();
		Console.WriteLine($"체력 {pd.hp} -> {Math.Max(0, pd.hp - minusHp)}");
		if (rewardGold > 0)
		{
			Console.WriteLine($"Gold {pd.gold} -> {pd.gold + rewardGold}");
			Console.WriteLine($"Level {pd.level} -> {pd.level + 1}");
			pd.level++;
			pd.defense++;
			pd.attack++; 
		}

		pd.hp = Math.Max(0, pd.hp - minusHp);
		pd.gold = pd.gold + rewardGold;

		Console.WriteLine();
		Console.WriteLine("0. 나가기");
		SpartaRPG.SelectOption(0, 0);
		EnterScene();
	}
}


