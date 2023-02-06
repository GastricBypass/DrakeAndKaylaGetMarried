using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int LatestScore;
    public bool NeedsToLogScore;

    private const string fileName = "highscores.dat";

    public List<ScoreEntry> ScoreEntries { get; set; }

    private void Awake()
    {
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        FileStream file;

        if (File.Exists(filePath))
        {
            file = File.OpenRead(filePath);

            BinaryFormatter bf = new BinaryFormatter();
            ScoreEntries = (List<ScoreEntry>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            ScoreEntries = new List<ScoreEntry>();
        }
    }

    public void SaveLatestScoreToFile(string name)
    {
        ScoreEntries.Add(new ScoreEntry(name, LatestScore));

        var filePath = Path.Combine(Application.persistentDataPath, fileName);
        FileStream file;

        if (File.Exists(filePath))
        {
            file = File.OpenWrite(filePath);
        }
        else
        {
            file = File.Create(filePath);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, ScoreEntries);
        file.Close();

        LatestScore = 0;
    }
}
