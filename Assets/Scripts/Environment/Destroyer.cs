using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public DestroyMode Mode;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Mode == DestroyMode.OnEnter)
        {
            Destroy(collision.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (Mode == DestroyMode.OnExit)
        {
            Destroy(collision.gameObject);
        }
    }
}

public enum DestroyMode
{
    OnEnter,
    OnExit
}