using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int Score;
    public bool NeedsToLogScore;

    private void Awake()
    {
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }
}
