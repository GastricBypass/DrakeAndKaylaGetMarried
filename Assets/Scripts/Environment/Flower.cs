using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public MovingItem Parent;
    public int Points = 2;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Parent.State.IncreaseScore(Points);
            Destroy(this.gameObject);
        }
    }
}
