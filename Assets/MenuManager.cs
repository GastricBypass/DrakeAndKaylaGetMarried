using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image KaylaReady;
    public Image DrakeReady;

    public GameObject HighscoreEntryPage;
    public Button EnterScoreButton;
    public TMP_Text Score;

    private bool _kaylaReady = false;
    private bool _drakeReady = false;

    void Start()
    {
        var scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (scoreKeeper.NeedsToLogScore)
        {
            scoreKeeper.NeedsToLogScore = false;
            ShowHighscoreEntryPage(scoreKeeper.Score);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("JumpKayla"))
        {
            ToggleKaylaReady();
        }

        if (Input.GetButtonDown("JumpDrake"))
        {
            ToggleDrakeReady();
        }

        if (_kaylaReady && _drakeReady)
        {
            SceneManager.LoadScene("Game");
        }
    }

    private void ToggleKaylaReady()
    {
        _kaylaReady = !_kaylaReady;
        KaylaReady.gameObject.SetActive(_kaylaReady);
    }

    private void ToggleDrakeReady()
    {
        _drakeReady = !_drakeReady;
        DrakeReady.gameObject.SetActive(_drakeReady);
    }

    private void ShowHighscoreEntryPage(int score)
    {
        HighscoreEntryPage.gameObject.SetActive(true);
        Score.text = score.ToString();
    }

    public void SaveHighscoreEntry()
    {
        Debug.Log(Score.text);
        HighscoreEntryPage.gameObject.SetActive(false);
    }
}
