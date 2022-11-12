using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TMP_Text ScoreText;

    public void UpdateScore(int newScore)
    {
        ScoreText.text = newScore.ToString();
    }
}
