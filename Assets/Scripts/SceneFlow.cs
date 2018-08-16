using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneList
{
	MainScene,
	Battle,
	Shop,
	MainTitle,
	MainMenu,
	Credits
}

public static class SceneFlow {

	static readonly List<string> scenes = new List<string> {
		"MainScene",
		"BattleScene",
		"ShopScene",
		"MainTitle",
		"MainMenu",
		"Credits"
	};


	static string GetSceneName(SceneList scene)
	{
		return scenes[(int)scene];
	}

	public static void RunScene(SceneList scene)
	{
		string sceneName = GetSceneName(scene);
		SceneManager.LoadScene(sceneName);
	}


}
