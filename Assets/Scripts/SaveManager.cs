using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * The save file works by breaking itself up into sections with 
 * 
 *					++++
 * 
 * If you want to add to the save file, create a section like this.
 * 
 * Each section is split between a header string and data
 * The section should look like: 
 * 
 *					"[Header]|||[Data]"
 *					
 * For example:
 * 
 *					Deck|||
 *					++++
 *					Enemies_defeated|||1010
 * 
 * Happy editing!
 */

public class SaveManager : MonoBehaviour {

	public bool loadgame;
	public bool saveFileFound;
	Dictionary<string, string> data;
	// Use this for initialization
	void Start () {
		try
		{
			string path = GetPath();
			PullDataFromSave(path);
			saveFileFound = true;
		}
		catch
		{
			saveFileFound = false;
			print("Savefile could not be found!");
		}
	}
	public void LoadData(GameManager gameManager)
	{
		if (saveFileFound)
		{

		}
	}

	private string GetPath()
	{
		return Application.dataPath + "\\saves\\saveFile.WOZSV";
	}

	private void PullDataFromSave(string path)
	{
		string text = File.ReadAllText(path);
		string[] lines;
	}
	void SaveFile(string path, string text)
	{
		File.Delete(path);
		File.WriteAllText(path, text);
		File.SetAttributes(path, FileAttributes.Hidden);
	}
}
