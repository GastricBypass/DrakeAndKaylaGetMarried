using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public Image KaylaReady;
    public Image DrakeReady;
    public float DelayTime;

    private bool _kaylaReady = false;
    private bool _drakeReady = false;

    private bool _waitingToLoad = false;

    private void LateUpdate()
    {
        if (Input.GetButtonDown("JumpKayla"))
        {
            ToggleKaylaReady();
        }

        if (Input.GetButtonDown("JumpDrake"))
        {
            ToggleDrakeReady();
        }

        if (_kaylaReady && _drakeReady && !_waitingToLoad)
        {
            StartCoroutine(WaitAndLoadGame());
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

    private IEnumerator WaitAndLoadGame()
    {
        _waitingToLoad = true;
        yield return new WaitForSeconds(DelayTime);
        _waitingToLoad = false;

        if (_kaylaReady && _drakeReady)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
