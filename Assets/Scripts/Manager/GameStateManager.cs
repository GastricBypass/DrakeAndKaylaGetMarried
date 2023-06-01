using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public UiManager Ui;

    public float Speed = 1;
    public float IncreaseRate = 0.001f;

    public int Score = 0;
    public float PassiveScoreIncreaseInterval = 1;

    private Player[] _players;

    private bool _gameOver;

    private void Start()
    {
        _players = FindObjectsOfType<Player>();

        StartCoroutine(PassiveScoreIncrease());
    }

    private void Update()
    {
        var allPlayersDead = true;
        foreach (var player in _players)
        {
            if (!player.IsDead)
            {
                allPlayersDead = false;
            }
        }

        if (allPlayersDead && !_gameOver)
        {
            StartGameOver();
        }
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
        if (!_gameOver)
        {
            Score += amount;
            Ui.UpdateScore(Score);
        }
    }

    private void StartGameOver()
    {
        _gameOver = true;
        var scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.LatestScore = Score;
        scoreKeeper.NeedsToLogScore = true;

        // Show game over screen

        StartCoroutine(WaitAndLoadMenu(2));
    }

    private IEnumerator WaitAndLoadMenu(float time)
    {
        yield return new WaitForSeconds(time);

        _gameOver = false;
        SceneManager.LoadScene("Menu");
    }
}
