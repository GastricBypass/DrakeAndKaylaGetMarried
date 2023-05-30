using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBouncer : MonoBehaviour
{
    public Character Character;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            Character.Bounce();
        }
    }
}
