using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSpacingRandomizer : MonoBehaviour
{
    public SegmentSpawner Spawner;

    public float MinSegmentWidth;
    public float MaxSegmentWidth;
    public float MinChangeInterval;
    public float MaxChangeInterval;

    private void Start()
    {
        RandomizeSpacing();
    }

    private IEnumerator WaitAndRandomize(float interval)
    {
        yield return new WaitForSeconds(interval);
        RandomizeSpacing();
    }

    private void RandomizeSpacing()
    {
        Spawner.SegmentWidth = Random.Range(MinSegmentWidth, MaxSegmentWidth);
        var randomInterval = Random.Range(MinChangeInterval, MaxChangeInterval);

        StartCoroutine(WaitAndRandomize(randomInterval));
    }
}
