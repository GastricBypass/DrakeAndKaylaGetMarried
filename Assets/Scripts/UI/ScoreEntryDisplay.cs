using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEntryDisplay : MonoBehaviour
{
    public ScoreEntry Entry;
    public TMP_Text Name;
    public TMP_Text Score;
    public int Rank;

    private void Update()
    {
        if (Name.text != Entry.Name || Score.text != Entry.Score.ToString())
        {
            Name.text = Rank + ": " + Entry.Name;
            Score.text = Entry.Score.ToString();
        }
    }
}
