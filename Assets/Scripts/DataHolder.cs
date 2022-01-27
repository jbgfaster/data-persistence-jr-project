using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance;
    public string playerName="Player1";
    public int playerScore;

    public string HightScoreName = "Player1";
    public int HightScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }
    [System.Serializable]
    class SaveData
    {
        public string HightScoreName;
        public int HightScore;
    }
    public void UpdateScore(int score)
    {
        if(score> HightScore)
        {
            HightScoreName = playerName;
            HightScore = score;
            SaveScore();
        }
    }
    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.HightScoreName = HightScoreName;
        data.HightScore = HightScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HightScoreName = data.HightScoreName;
            HightScore = data.HightScore;
        }
    }

}
