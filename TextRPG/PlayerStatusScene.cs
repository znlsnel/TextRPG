using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
	public class PlayerStatusScene : Scene
	{
		public PlayerStatusScene(string name) : base(name)
		{

		}


		public override void StartScene()
		{
			Console.Clear();
			Console.WriteLine("캐릭터의 정보입니다."); 
			Console.WriteLine();

			PlayerData pd = DataManager.Instance.playerData;
			Console.WriteLine($"Lv. {pd.level}");

			int addAttack = 0;
			int addArmor = 0;
			List<Item> items = DataManager.Instance.GetPlayerItem(true);
			foreach (Item item in items)
			{
				if (item.type == EItemType.WEAPON)
					addAttack += item.value;
				else
					addArmor += item.value;
			}

			string jobName = pd.job == EJobType.WARRIOR ? "전사" : pd.job == EJobType.ROGUE ? "도적" : "마법사";
			Console.WriteLine($"{pd.name} ( {jobName} )");
			Console.WriteLine($"공격력 : {pd.attack + addAttack} (+{addAttack})");
			Console.WriteLine($"방어력 : {pd.armor + addArmor} (+{addArmor})");
			Console.WriteLine($"체력 : {pd.maxHp}");
			Console.WriteLine($"Gold : {pd.gold}G");
			Console.WriteLine();
			Console.WriteLine("0. 나가기");

			int value = GameManager.Instance.GetPlayerInputInt(0, 0);
			if (value == 0)
				return;

		}
	}
}
