using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadingElement : MonoBehaviour
{
    public TMP_Text Text;
    public float FadeRate = 1;

    void FixedUpdate()
    {
        Text.color = new Color(
            Text.color.r,
            Text.color.g, 
            Text.color.b, 
            Text.color.a - (FadeRate * Time.deltaTime));

        if (Text.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
