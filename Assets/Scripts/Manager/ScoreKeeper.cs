using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int LatestScore;
    public bool NeedsToLogScore;

    public List<ScoreEntry> ScoreEntries { get; set; }

    private void Awake()
    {
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        ScoreEntries = new List<ScoreEntry>(); // TODO: Make this load from a file
    }

    public void SaveLatestScoreToFile(string name)
    {
        ScoreEntries.Add(new ScoreEntry(name, LatestScore));
        // TODO: Save to file

        LatestScore = 0;
    }
}
