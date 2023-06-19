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

    private bool _kaylaReady = false;
    private bool _drakeReady = false;

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
}
