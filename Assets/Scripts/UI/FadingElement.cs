using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadingElement : MonoBehaviour
{
    public Graphic Graphic;
    public float Delay = 0;
    public float FadeRate = 1;
    public bool FadeIn = false;
    public bool DestroyOnComplete = false;

    private bool delaying;

    private void Start()
    {
        StartCoroutine(WaitForDelay());
    }

    private void FixedUpdate()
    {
        if (delaying)
        {
            return;
        }

        if (FadeIn)
        {
            DoFadeIn();
        }
        else 
        {
            DoFadeOut();
        }
    }

    private void DoFadeIn()
    {
        if (Graphic.color.a < 1)
        {
            Graphic.color = new Color(
                Graphic.color.r,
                Graphic.color.g,
                Graphic.color.b,
                Graphic.color.a + (FadeRate * Time.deltaTime));
        }

        if (DestroyOnComplete && Graphic.color.a >= 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void DoFadeOut()
    {
        if (Graphic.color.a > 0)
        {
            Graphic.color = new Color(
                Graphic.color.r,
                Graphic.color.g,
                Graphic.color.b,
                Graphic.color.a - (FadeRate * Time.deltaTime));
        }

        if (DestroyOnComplete && Graphic.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator WaitForDelay()
    {
        delaying = true;
        yield return new WaitForSeconds(Delay);
        delaying = false;
    }
}
