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
    private ScoreKeeper _scoreKeeper;
    private bool _gameOver;
    private int _previousHighscore;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _players = FindObjectsOfType<Player>();

        _previousHighscore = _scoreKeeper.GetHighscore();

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

    public void IncreaseScore(int amount, bool notify = false)
{
        if (!_gameOver)
        {
            Score += amount;
            Ui.UpdateScore(Score, notify, amount > 0);

            if (Score > _previousHighscore)
            {
                Ui.NotifyNewHighscore();
                _scoreKeeper.GotNewHighscore = true;
            }
        }
    }

    private void StartGameOver()
    {
        _gameOver = true;
        _scoreKeeper.LatestScore = Score;
        _scoreKeeper.NeedsToLogScore = true;

        Ui.NotifyGameOver();

        StartCoroutine(WaitAndLoadMenu(2.5f));
    }

    private IEnumerator WaitAndLoadMenu(float time)
    {
        yield return new WaitForSeconds(time);

        _gameOver = false;
        SceneManager.LoadScene("Menu");
    }
}
