using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreEntryManager : MonoBehaviour
{
    public TMP_Text Score;
    public List<TMP_Text> KaylaNameInitials;
    public List<TMP_Text> DrakeNameInitials;

    private int _kaylaSelectedTextIndex = 0;
    private int _drakeSelectedTextIndex = 0;

    private bool _kaylaLockedIn;
    private bool _drakeLockedIn;

    private bool _drakeChangingIndex = false;
    private bool _kaylaChangingIndex = false;

    public bool NeedsToLogScore => _scoreKeeper.NeedsToLogScore;

    private ScoreKeeper _scoreKeeper;

    private void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        Score.text = _scoreKeeper.LatestScore.ToString();
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("JumpKayla"))
        {
            if (_kaylaSelectedTextIndex < KaylaNameInitials.Count)
            {
                IncrementInitial(KaylaNameInitials, _kaylaSelectedTextIndex);
            }
            else
            {
                _kaylaLockedIn = !_kaylaLockedIn;
            }
        }

        if (Input.GetButtonDown("JumpDrake"))
        {
            if (_drakeSelectedTextIndex < DrakeNameInitials.Count)
            {
                IncrementInitial(DrakeNameInitials, _drakeSelectedTextIndex);
            }
            else
            {
                _drakeLockedIn = !_drakeLockedIn;
            }
        }

        if (!_kaylaChangingIndex && Input.GetAxis("HorizontalKayla") > 0)
        {
            _kaylaChangingIndex = true;
            if (_kaylaSelectedTextIndex < KaylaNameInitials.Count + 1)
            {
                _kaylaSelectedTextIndex++;
            }
        }
        else if (!_kaylaChangingIndex && Input.GetAxis("HorizontalKayla") < 0)
        {
            _kaylaChangingIndex = true;
            if (_kaylaSelectedTextIndex > 0)
            {
                _kaylaSelectedTextIndex--;
            }
        }
        else if (Input.GetAxis("HorizontalDrake") == 0)
        {
            _kaylaChangingIndex = false; // some bullshit because we're using an axis istead of a button
        }

        if (!_drakeChangingIndex && Input.GetAxis("HorizontalDrake") > 0)
        {
            _drakeChangingIndex = true;
            if (_drakeSelectedTextIndex < DrakeNameInitials.Count + 1)
            {
                _drakeSelectedTextIndex++;
            }
        }
        else if (!_drakeChangingIndex && Input.GetAxis("HorizontalDrake") < 0)
        {
            _drakeChangingIndex = true;
            if (_drakeSelectedTextIndex > 0)
            {
                _drakeSelectedTextIndex--;
            }
        }
        else if (Input.GetAxis("HorizontalDrake") == 0)
        {
            Debug.Log("Resetting");
            _drakeChangingIndex = false; // some bullshit because we're using an axis istead of a button
        }

        if (_kaylaLockedIn && _drakeLockedIn)
        {
            SaveHighscoreEntry();
        }
    }

    private void SaveHighscoreEntry()
    {
        Debug.Log(_scoreKeeper.LatestScore);
        _scoreKeeper.NeedsToLogScore = false;
        var kaylaName = AggregateInitials(KaylaNameInitials);
        var drakeName = AggregateInitials(DrakeNameInitials);

        _scoreKeeper.SaveLatestScoreToFile(kaylaName + " & " + drakeName);
    }

    private string AggregateInitials(List<TMP_Text> initialInputs)
    {
        var name = string.Empty;
        foreach (var initial in initialInputs)
        {
            name += initial.text;
        }

        return name;
    }

    private void IncrementInitial(List<TMP_Text> initials, int index)
    {
        var currentInitial = initials[index].text.ToLower().ToCharArray()[0];
        initials[index].text = GetNextLetter(currentInitial).ToString().ToUpper();
    }

    private char GetNextLetter(char c)
    {
        c++;
        if (c > 'z')
        {
            c = 'a';
        }

        return c;
    }
}
