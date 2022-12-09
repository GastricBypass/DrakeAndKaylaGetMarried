using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpiringItem : MonoBehaviour
{
    public float TimeToLive = 8;

    void Start()
    {
        StartCoroutine(DestroyAfterTime(TimeToLive));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
