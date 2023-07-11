using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageArea : MonoBehaviour
{
    public float Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null)
        {
            Debug.Log(Time.time + ": " + character.name + " collides with " + name);
            Debug.Log(Time.time + ": " + name + " was at position " + transform.position);
            character.TakeDamage(Damage);
        }
    }
}
