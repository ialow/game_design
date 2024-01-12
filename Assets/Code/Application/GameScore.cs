using Ddd.Application;
using Ddd.Domain;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameScore
{
    private string filePath = Application.persistentDataPath + "/Score.gamesave";
    public static int Score { get; private set; } = 0;

    public static Action<int> UIEvent;

    public GameScore()
    {
        AiNpc.DeathEvent += AddScore;
    }

    private void AddScore(int countAddScore)
    {
        if (countAddScore > 0)
            Score += countAddScore;
        UIEvent?.Invoke(Score);
    }

    private bool CheckRecordHighScore
    {
        get
        {
            var bf = new BinaryFormatter();

            var stream = new FileStream(filePath, FileMode.Open);
            var lastData = (SaveData)bf.Deserialize(stream);
            stream.Close();

            return lastData.Score < Score ? true : false;
        }
    }

    private void SerializeData(BinaryFormatter bf)
    {
        var stream = new FileStream(filePath, FileMode.Create);
        var save = new SaveData(Score);

        bf.Serialize(stream, save);
        stream.Close();
    }

    public void SaveScore()
    {
        var bf = new BinaryFormatter();

        if (!File.Exists(filePath))
            SerializeData(bf);
        else if (CheckRecordHighScore)
            SerializeData(bf);
        Debug.Log("Save");
    }

    public int LoadScore()
    {
        if (!File.Exists(filePath)) return 0;
        Debug.Log(filePath);
        var bf = new BinaryFormatter();
        var stream = new FileStream(filePath, FileMode.Open);

        var save = (SaveData)bf.Deserialize(stream);
        stream.Close();

        return save.Score;
    }
}

[System.Serializable]
public class SaveData
{
    public int Score;

    public SaveData(int score)
    {
        Score = score;
    }
}