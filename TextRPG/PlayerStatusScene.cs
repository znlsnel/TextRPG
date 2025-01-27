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
			Console.WriteLine();
			Console.WriteLine("캐릭터의 정보입니다.");
			PlayerData pd = DataManager.Instance.playerData;
			Console.WriteLine($"Lv. {pd.level}");

			string jobName = pd.job == EJobType.WARRIOR ? "전사" : pd.job == EJobType.ROGUE ? "도적" : "마법사";
			Console.WriteLine($"{pd.name} ( {jobName} )");
			Console.WriteLine($"공격력 : {pd.attack}");
			Console.WriteLine($"방어력 : {pd.armor}");
			Console.WriteLine($"체력 : {pd.health}");
			Console.WriteLine($"Gold : {pd.gold}G");
			Console.WriteLine();
			Console.WriteLine("0. 나가기");

			int value = GameManager.Instance.GetPlayerInputInt();

		}
	}
}
