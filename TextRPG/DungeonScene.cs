using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

public class DungeonScene : Scene
{
	public DungeonScene(string name) : base(name){}
	Random rand = new Random();

	int[] levels = new int[3] { 5, 11, 17 };
	int[] rewards = new int[3] {1000, 1700, 2500};
	string[] names = new string[3] { "쉬움", "일반", "어려운"};

	public override void StartScene() 
	{
		Console.Clear();
		Console.WriteLine("[던전입장]");
		Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
		Console.WriteLine("");
		Console.WriteLine($"1. {names[0]} 던전 \t 방어력 {levels[0]} 이상 권장");
		Console.WriteLine($"2. {names[1]} 던전 \t 방어력 {levels[1]} 이상 권장");
		Console.WriteLine($"3. {names[2]} 던전 \t 방어력 {levels[2]} 이상 권장");
		Console.WriteLine("0. 나가기");

		int value = GameManager.Instance.SelectOption(0, 3);

		if (value != 0)
			EnterDungeon(value-1);
	}

	public void EnterDungeon(int level)
	{
		Console.Clear();
		PlayerData pd = DataManager.Instance.playerData;

		if (pd.hp == 0)
		{
			Console.WriteLine("[던전 실패..]");
			Console.WriteLine($"현재 체력이 0입니다. 회복 후에 다시 도전 해주세요.");

			Console.WriteLine();
			Console.WriteLine("0. 나가기");
			GameManager.Instance.SelectOption(0, 0);
			StartScene();
			return;
		}

		int minusHp = Math.Max(3, rand.Next(20 - pd.armor, 36 - pd.armor)); 
		int rewardGold = 0;

		if (pd.hp <= minusHp || ( pd.armor < levels[level] && rand.Next(0, 100) < 40))
		{
			Console.WriteLine("[던전 클리어 실패..]");
			Console.WriteLine($"{names[level]} 던전 클리어에 실패 하셨습니다...");

			if (pd.hp > minusHp)
				minusHp = pd.maxHp / 2;
		}
		else
		{
			Console.WriteLine("[던전 클리어]"); 
			Console.WriteLine($"축하합니다!!");
			Console.WriteLine($"{names[level]} 던전을 클리어 하였습니다!");
			int add = (rewards[level] * pd.attack / 100);
			rewardGold = rewards[level] + rand.Next(add, add*2); 
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
			pd.armor++;
			pd.attack++;
		}

		pd.hp = Math.Max(0, pd.hp - minusHp);
		pd.gold = pd.gold + rewardGold; 

		Console.WriteLine();
		Console.WriteLine("0. 나가기");
		GameManager.Instance.SelectOption(0, 0);
		StartScene();
	    }

}
