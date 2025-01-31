using System;
using System.Collections.Generic;
using System.Threading;

using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Linq;


public class SpartaRPG
{
	DataManager dataManager = new DataManager(); 
	Town town = new Town(); 

	public void GameStart()
	{
		Console.Clear();
		Console.WriteLine("스파르타 RPG에 접속하신 것을 환영합니다.");

		Console.WriteLine();
		Console.WriteLine("1. 새로시작");
		Console.WriteLine("2. 불러오기");
		Console.WriteLine("0. 게임 종료"); 

		
		int value = SelectOption(0, 2);

		if (value == 1)
			CreateCharacter();
		else if (value == 2)
			LoadCharacter();
	}

	void CreateCharacter()
	{
		Console.Clear();
		Console.WriteLine("원하시는 직업을 선택해 주세요");

		DataManager.Instance.PrintJobInfos();

		int idx = SelectOption(1, 3);

		Console.WriteLine();
		Console.Write("캐릭터의 이름을 입력해주세요 : ");
		string name = Console.ReadLine();
		DataManager.Instance.CreateCharacter(name, idx);
		 
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			Console.Write(" .");
		}
		Console.WriteLine();
		Console.WriteLine();

		Console.WriteLine("캐릭터 생성이 완료 되었습니다. \n게임에 접속합니다");
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			Console.Write(" .");
		}
		town.EnterTown();
	}
	 
	void LoadCharacter()
	{
		Console.WriteLine();
		Console.WriteLine("기존에 플레이 했던 캐릭터의 이름을 입력해주세요");

		string name = Console.ReadLine();
			Console.Clear();
		if (DataManager.Instance.LoadData(name))
		{
			Console.WriteLine("데이터를 불러오는데 성공했습니다!");

			Console.WriteLine("\n1. 게임시작");
			Console.WriteLine("0. 나가기");
			if (SelectOption(0, 1) == 0)
				GameStart(); 
			 
			else
				town.EnterTown();
		}
		else
		{
			Console.WriteLine("데이터를 찾을 수 없습니다.");
			Console.WriteLine("\n0. 돌아가기");
			SelectOption(0, 0); 
			GameStart();
		}
	} 

	public static int SelectOption(int a, int b) 
	{
		Console.WriteLine("\n원하시는 행동을 입력해주세요");
		Console.Write(">> ");

		string str;
		bool isNumeric;
		int ret;

		while (true)
		{
			str = Console.ReadLine();
			isNumeric = str.All(char.IsDigit);
			ret = isNumeric && str.Length > 0? int.Parse(str) : a - 1 ;

			if (isNumeric && ret >= a && ret <= b) 
				break;

			Console.WriteLine("\n잘못된 입력입니다. 다시 입력해주세요.");
			Console.Write(">> ");
		}

		return ret;
	}
}
