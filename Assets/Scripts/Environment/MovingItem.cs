using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{
    public GameStateManager State;

    public float MovementMultiplier = 1;

    private void FixedUpdate()
    {
        transform.position += new Vector3(-State.Speed * Time.deltaTime * MovementMultiplier, 0, 0);
    }
}
