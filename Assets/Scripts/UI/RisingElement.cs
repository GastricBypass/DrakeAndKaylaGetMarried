using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingElement : MonoBehaviour
{
    public float RiseRate = 1;

    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x, 
            transform.localPosition.y + (RiseRate * Time.deltaTime), 
            transform.localPosition.z);
    }
}
