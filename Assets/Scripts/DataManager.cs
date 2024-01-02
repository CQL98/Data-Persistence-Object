using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Linq;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string actualPlayerName;
    public int actualScore;
    public int numberOfTopPlayers = 3; 
    public List<PlayerScore> listPlayerScore = new List<PlayerScore>(); 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadUsers();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SavePlayer()
    {
        PlayerScore localScore = new PlayerScore();
        localScore.nameX = actualPlayerName;
        localScore.scoreX = actualScore;
        localScore.dateX = FormatDateTime(DateTime.Now);

        listPlayerScore.Add(localScore);
        listPlayerScore = listPlayerScore.OrderByDescending(playerScore => playerScore.scoreX).ToList(); 
        listPlayerScore = listPlayerScore.GetRange(0, Mathf.Min(numberOfTopPlayers, listPlayerScore.Count)); // Only keep 3 register

        ListContainer listContainer = new ListContainer();
        listContainer.listScore = listPlayerScore;
        string json = JsonUtility.ToJson(listContainer); 

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }
    public void LoadUsers()
    { 
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        { 
            string json = File.ReadAllText(path); 
            ListContainer localList = new ListContainer();
            localList = JsonUtility.FromJson<ListContainer>(json); 
            listPlayerScore = localList.listScore;
            listPlayerScore.Sort((x, y) => y.scoreX.CompareTo(y.scoreX)); 
        }
    }

    public PlayerScore GetTopUser()
    {
        LoadUsers();
        if (listPlayerScore.Count > 0)
        {
            return listPlayerScore[0];
        }
        PlayerScore dummyScore = new PlayerScore();
        dummyScore.nameX = "-----";
        dummyScore.scoreX = 0;
        return dummyScore;
    }

    public static string FormatDateTime(DateTime dateTime)
    {
        string dateString = dateTime.ToString("dd/MM/yyyy");
        return dateString;
    }
    public static string FormatScore(int score)
    {
        string scoreString = score.ToString("00000");
        return scoreString;
    }
    static int nameLenghtToShow = 15;
    public static string FormatName(string name)
    {
        string textNormalized = name.PadLeft((nameLenghtToShow - name.Length) / 2 + name.Length).PadRight(nameLenghtToShow);
        return textNormalized;
    }

}

[System.Serializable]
public class PlayerScore
{
    public int scoreX;
    public string nameX;
    public string dateX;
}

[System.Serializable]
public class ListContainer
{
    public List<PlayerScore> listScore;
}
