using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    public List<GameObject> PossibleObjects;

    private void Start()
    {
        var objectIndex = Random.Range(0, PossibleObjects.Count);
        var newObject = Instantiate(PossibleObjects[objectIndex]);

        newObject.transform.position = transform.position;
        newObject.transform.parent = transform.parent;

        Destroy(gameObject);
    }
}
