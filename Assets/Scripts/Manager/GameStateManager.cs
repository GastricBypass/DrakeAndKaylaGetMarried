using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public UiManager Ui;

    public float Speed = 1;
    public float IncreaseRate = 0.001f;

    public int Score = 0;
    public float PassiveScoreIncreaseInterval = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PassiveScoreIncrease());
    }

    private void FixedUpdate()
    {
        Speed += IncreaseRate * Time.deltaTime;
    }

    private IEnumerator PassiveScoreIncrease()
    {
        yield return new WaitForSeconds(PassiveScoreIncreaseInterval);
        IncreaseScore(1);

        StartCoroutine(PassiveScoreIncrease());
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        Ui.UpdateScore(Score);
    }
}
