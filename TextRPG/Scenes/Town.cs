﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Town
{
	Scene _statusScene = new PlayerStatusScene("상태 보기");
	Scene _inventoryScene = new InventoryScene("인벤토리");
	Scene _storeScene = new StoreScene("상점");
	Scene _restScene = new RestScene("휴식 하기");
	Scene _dungeon = new DungeonScene("던전입장");
	Scene _save = new SaveScene("저장하기");

	List<Scene> _scenes = new List<Scene>();

	public Town()
	{
		// 각 Scene을 배열에 담고 순회하며 진입할 수 있도록 설계
		_scenes = new List<Scene>() { 
			_statusScene, 
			_inventoryScene,
			_storeScene, 
			_restScene, 
			_dungeon, 
			_save
		};
	}


	public void EnterTown()
	{
		// 반복문을 통해 마을을 벗어날 수 없게 설계
		while (true)
		{
			Console.Clear();
			Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
			Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
			Console.WriteLine();

			for (int i = 0; i < _scenes.Count; i++)
				Console.WriteLine($"{i + 1}. {_scenes[i].GetName()}");

			Console.WriteLine("0. 나가기");

			// 0 입력시 반복문 벗어나며 종료
			int value = SpartaRPG.SelectOption(0, _scenes.Count + 1);
			if (value == 0)
				return;

			else 
				_scenes[value - 1].EnterScene();
		}
	}

}

