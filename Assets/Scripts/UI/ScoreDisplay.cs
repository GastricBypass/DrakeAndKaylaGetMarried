using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public ScoreEntryDisplay ScoreEntryPrefab;
    private ScoreKeeper _scoreKeeper;

    private void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            Destroy(child.gameObject);
        }

        var orderedEntries = _scoreKeeper.ScoreEntries.OrderByDescending(x => x.Score).ToList();
        for (int i = 0; i < orderedEntries.Count; i++)
        {
            var display = Instantiate(ScoreEntryPrefab, transform) as ScoreEntryDisplay;
            display.Entry = orderedEntries[i];
            display.Rank = i + 1;
        }
    }
}
