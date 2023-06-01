using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public MovingItem Parent;
    public int Points = 2;

    private void Start()
    {
        if (Parent == null)
        {
            Parent = GetComponentInParent<MovingItem>();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null && !player.IsDead)
        {
            player.Pickup();
            Parent.State.IncreaseScore(Points);
            Destroy(this.gameObject);
        }
    }
}
