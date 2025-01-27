using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
	

	public class TownScene
	{
		Scene _statusScene = new PlayerStatusScene("상태 보기");
		Scene _inventoryScene = new InventoryScene("인벤토리");
		Scene _storeScene = new StoreScene("상점");

		List<Scene> _scenes = new List<Scene>();

		public TownScene()  
		{
			_scenes.Add(_statusScene);
			_scenes.Add(_inventoryScene);
			_scenes.Add(_storeScene);
		}

		public void OpenTown()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
				Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
				Console.WriteLine();

				for (int i = 0; i < _scenes.Count; i++)
					Console.WriteLine($"{i + 1}. {_scenes[i].GetName()}");

				Console.WriteLine("0. 나가기");

				int value = GameManager.Instance.GetPlayerInputInt();
				if (value == 0)
					return;

				_scenes[value - 1].StartScene();
			}
		}
	}
}
