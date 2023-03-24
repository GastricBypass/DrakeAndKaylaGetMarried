using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameStartManager GameStartPage;
    public ScoreEntryManager ScoreEntryPage;
    public ScoreDisplay ScoreDisplay;

    private void LateUpdate()
    {
        try 
        {
            if (ScoreEntryPage.NeedsToLogScore && (GameStartPage.gameObject.activeSelf || !ScoreEntryPage.gameObject.activeSelf))
            {
                ShowScoreEntryPage();
            }
            else if (!ScoreEntryPage.NeedsToLogScore && (!GameStartPage.gameObject.activeSelf || ScoreEntryPage.gameObject.activeSelf))
            {
                ShowGameStartPage();
            }
        }
        catch
        {
            Debug.Log("Should really handle this better");
        }
    }

    private void ShowScoreEntryPage()
    {
        GameStartPage.gameObject.SetActive(false);
        ScoreEntryPage.gameObject.SetActive(true);
    }

    private void ShowGameStartPage()
    {
        ScoreEntryPage.gameObject.SetActive(false);
        GameStartPage.gameObject.SetActive(true);
        ScoreDisplay.Refresh();
    }
}
