using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public MovingItem Parent;
    public int Points = 2;

    private bool _pickedUp;

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
        if (!_pickedUp && player != null && !player.IsDead)
        {
            _pickedUp = true; // for  the scenario where both players hit the flower in the same frame
            player.Pickup();
            Parent.State.IncreaseScore(Points, true);
            Destroy(this.gameObject);
        }
    }
}
