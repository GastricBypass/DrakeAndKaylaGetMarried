using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    public GameStateManager State;

    public List<MovingItem> SegmentPrefabs;
    public Vector2 SpawnPosition;

    [Tooltip("Equivalent to the width of the segment")]
    public float SegmentWidth = 20;
    public float MovementMultiplier = 1;

    private MovingItem _previousItem;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnSegment();
        //StartCoroutine(WaitToSpawn(SpawnInterval));
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (_previousItem == null) 
        {
            SpawnSegment(SpawnPosition);
        }
        else if (SpawnPosition.x - _previousItem.transform.position.x >= SegmentWidth)
        {
            SpawnSegment(new Vector2(_previousItem.transform.position.x + SegmentWidth, _previousItem.transform.position.y));
        }
    }

    //private IEnumerator WaitToSpawn(float interval)
    //{
    //    yield return new WaitForSeconds(interval);
    //    SpawnSegment();

    //    StartCoroutine(WaitToSpawn(SpawnInterval / State.Speed));
    //}

    private void SpawnSegment(Vector2 position)
    {
        var itemIndex = Random.Range(0, SegmentPrefabs.Count);
        var newPlatform = Instantiate<MovingItem>(SegmentPrefabs[itemIndex]);

        newPlatform.State = State;
        newPlatform.MovementMultiplier = MovementMultiplier;
        newPlatform.transform.position = position;

        _previousItem = newPlatform;

        // let this fully pass through the camera and then destroy it, we should only have 2 active at once
        StartCoroutine(DestroyAfterTime(newPlatform.gameObject, SegmentWidth / State.Speed / MovementMultiplier * 2));
    }

    private IEnumerator DestroyAfterTime(GameObject itemToDestroy, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(itemToDestroy);
    }
}
