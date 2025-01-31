using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
	public class RestScene : Scene
	{
		public RestScene(string name) : base(name){}
		int price = 500;

		public override void StartScene()
		{
			PlayerData pd = DataManager.Instance.playerData;
			Console.Clear();
			Console.WriteLine($"{price} G를 내면 체력을 회복할 수 있습니다 (보유 골두 : {pd.gold} G)");

			Console.WriteLine();
			Console.WriteLine("1. 휴식하기");
			Console.WriteLine("0. 나가기");

			int value = GameManager.Instance.SelectOption(0, 1);
			if (value == 1)
			{
				TakeRest();
			}

		}

		void TakeRest()
		{
			Console.Clear();

			PlayerData pd = DataManager.Instance.playerData;
			if (pd.gold < price)
				Console.WriteLine("Gold가 부족합니다");
			else if (pd.hp == pd.maxHp)
				Console.WriteLine("이미 체력이 회복된 상태입니다.");
			else
			{
				Console.WriteLine("체력을 회복 했습니다!");
				pd.gold -= price;
				pd.hp = pd.maxHp;
			}

			Console.WriteLine("\n0. 나가기");

			int value = GameManager.Instance.SelectOption(0, 0);
			
		}
	}
}
