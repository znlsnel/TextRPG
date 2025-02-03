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

		int classCnt = DataManager.Instance.PrintClassInfos();

		int idx = SelectOption(1, classCnt);
		 
		Console.WriteLine();
		Console.Write("캐릭터의 이름을 입력해주세요 : ");
		string name = Console.ReadLine();

		// 이미 생성된 아이디라면 계속 입력 받도록 설계
		while (DataManager.Instance.DoesIdExists(name))
		{
			Console.Write("\n이미 존재하는 이름입니다 다시 입력해주세요 : ");
			name = Console.ReadLine();
		}
		DataManager.Instance.CreateCharacter(name, idx);
		 
		// 10초간 대기하는 코드
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			Console.Write(" .");
		}

		Console.WriteLine("\n\n캐릭터 생성이 완료 되었습니다. \n게임에 접속합니다");

		// 10초간 대기하는 코드
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
			isNumeric = str.All(char.IsDigit); // 모든 문자가 숫자인지 체크
			ret = isNumeric && str.Length > 0? int.Parse(str) : a - 1 ; // 인자로 받은 범위 안의 수인지 체크

			// 조건에 부합하는 수를 입력받은 경우 반복문 종료
			if (isNumeric && ret >= a && ret <= b) 
				break;

			// 조건에 부합하지 않는다면 반복하여 입력 받기
			Console.WriteLine("\n잘못된 입력입니다. 다시 입력해주세요.");
			Console.Write(">> ");
		}

		return ret;
	}
}
