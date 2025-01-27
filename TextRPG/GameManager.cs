using System;
using System.Collections.Generic;
using TextRPG;

public class GameManager
{
	Scene _statusScene = new PlayerStatusScene("상태 보기");
	Scene _inventoryScene = new InventoryScene("인벤토리");
	Scene _storeScene = new StoreScene("상점");	

	List<Scene> _scenes = new List<Scene>();

	public GameManager()
	{
		_scenes.Add(_statusScene);
		_scenes.Add(_inventoryScene);
		_scenes.Add(_storeScene);
	}

	public void OpenTown()
	{
		Console.Clear();
		Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
		Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
		Console.WriteLine();

		for (int i = 0; i < _scenes.Count; i++)
			Console.WriteLine($"{i+1}. {_scenes[i].GetName()}");

		Console.WriteLine();
		Console.WriteLine("원하시는 행동을 입력해주세요.");
		Console.Write(">> ");
		int value = int.Parse(Console.ReadLine());

		_scenes[value].StartScene(); 
	}

	public void GameStart()
	{
		Console.Clear();
		Console.WriteLine("스파르타 RPG에 접속하신 것을 환영합니다.");

		Console.WriteLine("1. 캐릭터 만들기");
		Console.WriteLine("0. 게임 종료");

		Console.WriteLine("원하시는 행동을 입력해주세요.");
		Console.Write(">> ");
		int value = int.Parse(Console.ReadLine());

		if (value == 0)
			return;

		if (value == 1)
		{

		}
	}
	

}
