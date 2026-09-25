using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;

public class Scorboard: MonoBehaviour
{
    [System.Serializable]
    public class PlayerScore { public string playerName; public string time; public float points; }
    public GameObject scoreUI;
    public List<PlayerScore> players = new List<PlayerScore>();
    public int number;
    void Start()
    {        
        //DestroyPlayerPrefs();

        CheckPlayerData();

    }
    void CheckPlayerData()
    {
        int i = 1;
        while (true)
        {
            string id = "Player" + i.ToString();
            if(PlayerPrefs.HasKey(id + "_Score"))
            {
                i++;
                PlayerScore ps = new PlayerScore();
                ps.playerName = id;
                ps.time = PlayerPrefs.GetString(id + "_Time", "");
                ps.points = PlayerPrefs.GetInt(id + "_Score", 0);
                players.Add(ps);
                AddText(id.ToString(), scoreUI, 10);
                AddText(ps.time.ToString(), scoreUI, 10);
                AddText(ps.points.ToString(), scoreUI, 10);
            }
            else
            {
                number = i;
                break;
            }
        }
        
    }
    void AddText(string text, GameObject ui, int fontSize)
    {
        GameObject textObject = new GameObject();
        textObject.transform.SetParent(ui.transform, false);
        TextMeshProUGUI tmpText = textObject.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = fontSize;
        tmpText.alignment = TextAlignmentOptions.Left;
        tmpText.color = Color.black;
        RectTransform rect = textObject.GetComponent<RectTransform>();
    }
    public void SavePlayer(string playerID, int score, string time, int numberOfMistakes)
    {
        PlayerPrefs.SetInt(playerID + "_Score", score);
        PlayerPrefs.SetString(playerID + "_Time", time);
        PlayerPrefs.SetInt(playerID + "_numberOfMistakes", numberOfMistakes);

        PlayerPrefs.Save();
    }
    int LoadPlayerScore(string playerID)
    {
        return PlayerPrefs.GetInt(playerID + "_Score", 0);
    }
    public void DestroyPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}

