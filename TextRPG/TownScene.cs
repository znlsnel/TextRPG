using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
	

	public class TownScene
	{
		Scene _statusScene = new PlayerStatusScene("상태 보기");
		Scene _inventoryScene = new InventoryScene("인벤토리");
		Scene _storeScene = new StoreScene("상점");
		Scene _restScene = new RestScene("휴식 하기");
		Scene _dungeon = new DungeonScene("던전입장");

		List<Scene> _scenes = new List<Scene>();

		public TownScene()  
		{
			_scenes.Add(_statusScene);
			_scenes.Add(_inventoryScene);
			_scenes.Add(_storeScene);
			_scenes.Add(_restScene);
			_scenes.Add(_dungeon); 
		}

		public void GameOn()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
				Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
				Console.WriteLine();

				for (int i = 0; i < _scenes.Count; i++)
					Console.WriteLine($"{i + 1}. {_scenes[i].GetName()}");

				Console.WriteLine($"{_scenes.Count+1}. 저장하기");
				Console.WriteLine("0. 나가기");

				int value = GameManager.Instance.GetPlayerInputInt(0, _scenes.Count+1);
				if (value == 0)
					return;

				if (value == _scenes.Count+1)
				{
					DataManager.Instance.SaveData();
					Console.WriteLine();
					Console.Write("게임을 저장합니다 ");
					
					for (int i = 0; i < 10; i++)
					{
						Thread.Sleep(50);
						Console.Write(".  "); 
					}

				    Console.WriteLine();
				    Console.WriteLine("게임 저장에 성공했습니다.");
				    Console.WriteLine("\n0. 돌아가기");
					GameManager.Instance.GetPlayerInputInt(0, 0);
				
				}
				else
					_scenes[value - 1].StartScene();
			}
		}
	}
}
