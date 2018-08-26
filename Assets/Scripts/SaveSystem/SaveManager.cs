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

	public bool saveGame;

	#region SaveFileStrings
	string filename = "saveFile.WOZSV";
	string folderName = "saves";

	static class SaveHeaders
	{
		public static string Deck = "Deck";
		public static string BossDefeatRecord = "Bosses_Defeated";
	}
	string sectionDivider = "++++";
	string header_dataDivider = "|||";
	#endregion

	public static SaveManager instance;

	public bool callLoadGameActive;

	#region SaveFileData
	public bool saveFileFound;

	Dictionary<string, string> data;
	#endregion


	// Use this for initialization
	private void Start()
	{
		if (saveGame)
		{
			print(GetPath());
			string text = SaveHeaders.BossDefeatRecord + header_dataDivider + "1110";
			SaveFile(GetPath(), text);
		}
		if (instance == null)
		{
			instance = this;
		}
		else if(instance != null)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		FindFile();
	}

	void FindFile() {
		try
		{
			string filePath = GetPath() + "/" + filename;
			print("Got path");
			PullDataFromSave(filePath);
			print("Got data from save");
			saveFileFound = true;
		}
		catch
		{
			saveFileFound = false;
			print("Savefile could not be found!");
		}
	}
	public void LoadGame(GameManager gameManager)
	{
		if (saveFileFound)
		{
			RestoreDeck(gameManager);
			RestoreBossDefeatedRecord(gameManager);
		}
	}

	public void RestoreDeck(GameManager gameManager)
	{

	}
	public void RestoreBossDefeatedRecord(GameManager gameManager)
	{
		//Boss Defeated Record String
		string bDRstring = data[SaveHeaders.BossDefeatRecord];
		for (int i = 0; i < bDRstring.Length; i++)
		{
			//Checks if the character in that position of the string
			//if equal to '1', then sets the enemy defeated tracker to true
			gameManager.enemiesDefeatedTracker[i] = (bDRstring[i] == '1');
		}
	}

	private string GetPath()
	{
		return Application.dataPath + "/" + folderName;
	}

	private void PullDataFromSave(string path)
	{
		data = new Dictionary<string, string>();
		string text = File.ReadAllText(path);
		string[] lines = SectionText(text);
		foreach (string line in lines)
		{
			int endOfHeader = line.IndexOf(header_dataDivider);
			int startOfData = endOfHeader + header_dataDivider.Length;
			string header = line.Substring(0, endOfHeader).Trim();
			print(header);
			string dataString = line.Substring(startOfData).Trim();
			print(dataString);
			data[header] = dataString;
			print("DataString " + dataString + " was put into data under header " + header);
		}
	}
	void CreateFile()
	{
		string newSaveFile = "";
		newSaveFile += AddToFile(SaveDeck());
		newSaveFile += AddToFile(sectionDivider);
		newSaveFile += SaveBossesDefeatedTracker();
	}
	string AddToFile(string str)
	{
		return str + "\n\r";
	}

	string SaveDeck()
	{
		return "";
	}
	string SaveBossesDefeatedTracker()
	{
		return "";
		foreach (bool bossDefeated in GameManager.instance.enemiesDefeatedTracker)
		{
			
		}
	}

	void SaveFile(string path, string text)
	{
		string filePath = path + "/" + filename;
		Directory.CreateDirectory(path);
		File.Delete(filePath);
		File.WriteAllText(filePath, text);
		File.SetAttributes(path, FileAttributes.Hidden);
	}
	string[] SectionText(string text)
	{
		List<string> linesList = new List<string>();
		int startOfSectionIndex = 0;
		for(int i = 0; i < text.Length - sectionDivider.Length; i++)
		{
			if(text.Substring(i, sectionDivider.Length) == sectionDivider)
			{
				linesList.Add(text.Substring(startOfSectionIndex, i - startOfSectionIndex));
				print(text.Substring(startOfSectionIndex, i - startOfSectionIndex));
				startOfSectionIndex = i + sectionDivider.Length;
			}
		}
		linesList.Add(text.Substring(startOfSectionIndex, text.Length - startOfSectionIndex));
		print(text.Substring(startOfSectionIndex, text.Length - startOfSectionIndex));
		string[] linesArray = linesList.ToArray();
		return linesArray;
	}
}
