using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Scene
{
	string sceneName;

	public Scene(string name) => sceneName = name;

	public string GetName() => sceneName;

	public abstract void EnterScene();
}

