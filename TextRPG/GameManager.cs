using System;
using System.Collections.Generic;
using System.Threading;
using TextRPG;

public class GameManager
{
	public static GameManager Instance;
	TownScene town = new TownScene();

	public GameManager()
	{
		Instance = this;
	}

	

	public void GameStart()
	{
		Console.Clear();
		Console.WriteLine("스파르타 RPG에 접속하신 것을 환영합니다.");

		Console.WriteLine();
		Console.WriteLine("1. 캐릭터 만들기");
		Console.WriteLine("0. 게임 종료");

		
		int value = GetPlayerInputInt();

		if (value == 0)
			return;

		if (value == 1)
		{
			Console.WriteLine();
			DataManager.Instance.PrintJobInfos();

			Console.WriteLine();
			Console.Write("원하시는 직업을 선택해 주세요 : "); 
			int idx = int.Parse(Console.ReadLine());

			Console.WriteLine();
			Console.Write("캐릭터의 이름을 입력해주세요 : ");
			string name = Console.ReadLine();
			DataManager.Instance.CreateNewCharacter(name, idx);

			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(100);
				Console.Write(" .");
			}

			Console.WriteLine();
			Console.WriteLine("캐릭터 생성이 완료 되었습니다. \n게임에 접속합니다");
			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(100);
				Console.Write(" .");
			} 
			town.OpenTown();
		}
	}
	
	public int GetPlayerInputInt()
	{
		Console.WriteLine();
		Console.WriteLine("원하시는 행동을 입력해주세요");
		Console.Write(">> ");
		return int.Parse(Console.ReadLine()); 
	}
}
