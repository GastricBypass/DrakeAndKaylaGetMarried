using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public Canvas Canvas;

    public TMP_Text ScoreText;

    public GameObject ScoreUpText;
    public GameObject ScoreDownText;
    public Vector3 ScoreNotificationSpawnPosition;

    public void UpdateScore(int newScore, bool notify, bool positive)
    {
        ScoreText.text = newScore.ToString();

        if (notify)
        {
            if (positive)
            {
                var scoreNotification = Instantiate(ScoreUpText, Canvas.transform);
                scoreNotification.transform.localPosition = ScoreNotificationSpawnPosition; 
            }
            else
            {
                var scoreNotification = Instantiate(ScoreDownText, Canvas.transform);
                scoreNotification.transform.localPosition = ScoreNotificationSpawnPosition;
            }
        }
    }
}
