using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    public GameStateManager State;

    public List<MovingItem> SegmentPrefabs;
    public Vector3 SpawnPosition;

    [Tooltip("Equivalent to the width of the segment")]
    public float SegmentWidth = 20;
    public float MovementMultiplier = 1;
    public float Lifetime = 20;

    private MovingItem _previousItem;

    private void LateUpdate()
    {
        if (_previousItem == null) 
        {
            SpawnSegment(SpawnPosition);
        }
        else if (SpawnPosition.x - _previousItem.transform.position.x >= SegmentWidth)
        {
            SpawnSegment(new Vector3(
                _previousItem.transform.position.x + SegmentWidth, 
                _previousItem.transform.position.y,
                _previousItem.transform.position.z));
        }
    }

    private void SpawnSegment(Vector3 position)
    {
        var itemIndex = Random.Range(0, SegmentPrefabs.Count);
        var newPlatform = Instantiate<MovingItem>(SegmentPrefabs[itemIndex], position, Quaternion.identity);

        newPlatform.State = State;
        newPlatform.MovementMultiplier = MovementMultiplier;

        _previousItem = newPlatform;

        // let this fully pass through the camera and then destroy it, we should only have 2 active at once
        StartCoroutine(DestroyAfterTime(newPlatform.gameObject, Lifetime / State.Speed / MovementMultiplier * 2));
    }

    private IEnumerator DestroyAfterTime(GameObject itemToDestroy, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(itemToDestroy);
    }
}
