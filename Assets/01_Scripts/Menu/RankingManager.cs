using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class RankingEntry
{
    public string playerName;
    public int score;
}

[Serializable]
public class RankingData
{
    public List<RankingEntry> entries = new List<RankingEntry>();
}

public class RankingManager : MonoBehaviour
{
    public static RankingManager Instance;

    private string path;
    public RankingData data = new RankingData();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        path = Path.Combine(Application.persistentDataPath, "ranking.json");
        Load();
    }

    public void AddScore(string name, int score)
    {
        data.entries.Add(new RankingEntry { playerName = name, score = score });
        data.entries = data.entries.OrderByDescending(e => e.score).Take(10).ToList();
        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<RankingData>(json);
        }
        else
        {
            data = new RankingData();
        }
    }
}