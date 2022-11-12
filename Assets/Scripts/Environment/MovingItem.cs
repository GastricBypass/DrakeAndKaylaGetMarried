using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{
    public GameStateManager State;

    private void FixedUpdate()
    {
        transform.position += new Vector3(-State.Speed * Time.deltaTime, 0, 0);
    }
}
