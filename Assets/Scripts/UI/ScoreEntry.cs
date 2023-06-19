using System;

[Serializable]
public struct ScoreEntry
{
    public string Name;
    public int Score;

    public ScoreEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
