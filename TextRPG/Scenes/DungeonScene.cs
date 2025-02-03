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
	public int armor;
	public int reward;
	public int exp;

	public DungeonInfo(string n, int l, int r, int e)
	{
		name = n;
		armor = l;
		reward = r;
		exp = e;
	}
}
public class DungeonScene : Scene
{
	public DungeonScene(string name) : base(name) { }
	Random rand = new Random();

	List<DungeonInfo> dungeons = new List<DungeonInfo>() 
	{
		new DungeonInfo("쉬움", 5, 1000, 1),
		new DungeonInfo("일반", 11, 2000, 2),
		new DungeonInfo("어려운", 17, 3000, 3), 
		new DungeonInfo("죽음의", 30, 10000, 5),
		new DungeonInfo("지옥의", 300, 100000, 10),
	};

	public override void EnterScene()
	{
		Console.Clear();
		Console.WriteLine("[던전입장]");
		Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
		Console.WriteLine("");
		
		for (int i = 0; i < dungeons.Count; i++)
			Console.WriteLine($"{i+1}. {dungeons[i].name} 던전 \t 방어력 {dungeons[i].armor} 이상 권장");
		
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

		int damage = dungeon.armor - pd.defense;
		int minusHp = Math.Max(rand.Next(0, 4), rand.Next(20, 36) + damage); 
		int rewardGold = 0;

		if (pd.hp <= minusHp || (pd.defense < dungeon.armor && rand.Next(0, 100) < 40))
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
			Console.WriteLine($"Level {pd.level} -> {pd.level + dungeon.exp}");
			pd.level += dungeon.exp;
			pd.defense += dungeon.exp;
			pd.attack += dungeon.exp;  
		}

		pd.hp = Math.Max(0, pd.hp - minusHp);
		pd.gold = pd.gold + rewardGold;

		Console.WriteLine();
		Console.WriteLine("0. 나가기");
		SpartaRPG.SelectOption(0, 0);
		EnterScene();
	}
}


