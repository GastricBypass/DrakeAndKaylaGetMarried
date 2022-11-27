using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Character Character;
    public float EnableTime = 1;

    private bool _enabled = false;
    private Player _currentPlayerInBubble = null;

    private void OnEnable()
    {
        StartCoroutine(WaitToEnable(EnableTime));
    }

    private void Update()
    {
        if (_enabled && _currentPlayerInBubble != null && !_currentPlayerInBubble.IsDead)
        {
            Character.SetBubbleEnabled(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null && !player.IsDead) 
        {
            _currentPlayerInBubble = player;
            if (_enabled)
            {
                Character.SetBubbleEnabled(false);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            _currentPlayerInBubble = null;
        }
    }

    private IEnumerator WaitToEnable(float seconds)
    {
        _enabled = false;
        yield return new WaitForSeconds(seconds);
        _enabled = true;
    }
}
