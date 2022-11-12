using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    public GameStateManager State;

    public List<MovingItem> PlatformPrefabs;
    public Vector2 SpawnPosition;

    [Tooltip("Equivalent to the width of the segment")]
    public float SpawnInterval = 2;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlatform();
        StartCoroutine(WaitToSpawn(SpawnInterval));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitToSpawn(float interval)
    {
        yield return new WaitForSeconds(interval);
        SpawnPlatform();

        StartCoroutine(WaitToSpawn(SpawnInterval / State.Speed));
    }

    private void SpawnPlatform()
    {
        var itemIndex = Random.Range(0, PlatformPrefabs.Count);
        var newPlatform = Instantiate<MovingItem>(PlatformPrefabs[itemIndex]);

        newPlatform.State = State;
        newPlatform.transform.position = new Vector2(SpawnPosition.x, SpawnPosition.y);

        // let this fully pass through the camera and then destroy it, we should only have 2 active at once
        StartCoroutine(DestroyAfterTime(newPlatform.gameObject, SpawnInterval / State.Speed * 2));
    }

    private IEnumerator DestroyAfterTime(GameObject itemToDestroy, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(itemToDestroy);
    }
}
