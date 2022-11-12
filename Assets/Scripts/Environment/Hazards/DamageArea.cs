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
            character.TakeDamage(Damage);
        }
    }
}
