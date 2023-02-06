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
    private bool _drakeChangingLetter = false;
    private bool _kaylaChangingIndex = false;
    private bool _kaylaChangingLetter = false;

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
            _kaylaLockedIn = !_kaylaLockedIn;
        }

        if (Input.GetButtonDown("JumpDrake"))
        {
            _drakeLockedIn = !_drakeLockedIn;
        }

        HandleLetterChangeInputs("VerticalKayla", KaylaNameInitials, _kaylaSelectedTextIndex, ref _kaylaChangingLetter);
        HandleLetterChangeInputs("VerticalDrake", DrakeNameInitials, _drakeSelectedTextIndex, ref _drakeChangingLetter);

        HandleIndexChangeInputs("HorizontalKayla", KaylaNameInitials, ref _kaylaSelectedTextIndex, ref _kaylaChangingIndex);
        HandleIndexChangeInputs("HorizontalDrake", DrakeNameInitials, ref _drakeSelectedTextIndex, ref _drakeChangingIndex);

        if (_kaylaLockedIn && _drakeLockedIn)
        {
            SaveHighscoreEntry();
        }
    }

    private void HandleLetterChangeInputs(string axis, List<TMP_Text> initials, int selectedIndex, ref bool changingLetter)
    {
        if (!changingLetter && Input.GetAxis(axis) > 0)
        {
            changingLetter = true;
            IncrementInitial(initials, selectedIndex, true);
        }
        else if (!changingLetter && Input.GetAxis(axis) < 0)
        {
            changingLetter = true;
            IncrementInitial(initials, selectedIndex, false);
        }
        else if (Input.GetAxis(axis) == 0)
        {
            changingLetter = false; // some bullshit because we're using an axis istead of a button
        }
    }

    private void HandleIndexChangeInputs(string axis, List<TMP_Text> initials, ref int selectedIndex, ref bool changingIndex)
    {
        if (!changingIndex && Input.GetAxis(axis) > 0)
        {
            changingIndex = true;
            if (selectedIndex < initials.Count - 1)
            {
                selectedIndex++;
            }
        }
        else if (!changingIndex && Input.GetAxis(axis) < 0)
        {
            changingIndex = true;
            if (selectedIndex > 0)
            {
                selectedIndex--;
            }
        }
        else if (Input.GetAxis(axis) == 0)
        {
            changingIndex = false; // some bullshit because we're using an axis istead of a button
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

    private void IncrementInitial(List<TMP_Text> initials, int index, bool up)
    {
        var currentInitial = initials[index].text.ToLower().ToCharArray()[0];
        initials[index].text = up ?
            GetNextLetter(currentInitial).ToString().ToUpper() :
            GetPrevLetter(currentInitial).ToString().ToUpper();
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

    private char GetPrevLetter(char c)
    {
        c--;
        if (c < 'a')
        {
            c = 'z';
        }

        return c;
    }
}
