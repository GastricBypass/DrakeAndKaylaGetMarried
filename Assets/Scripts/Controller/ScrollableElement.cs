using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableElement : MonoBehaviour
{
    public float ScrollSpeed = 0.5f;

    private bool shouldScrollUp;
    private bool shouldScrollDown;

    private float minHeight;

    private void Start()
    {
        minHeight = transform.position.y;
    }

    private void Update()
    {
        var kaylaInput = Input.GetAxis("VerticalKayla");
        var drakeInput = Input.GetAxis("VerticalDrake");

        var netInput = kaylaInput + drakeInput;

        if (netInput > 0)
        {
            shouldScrollUp = true;
            shouldScrollDown = false;
        }
        else if (netInput < 0)
        {
            shouldScrollUp = false;
            shouldScrollDown = true;
        }
        else
        {
            shouldScrollUp = false;
            shouldScrollDown = false;
        }
    }

    private void FixedUpdate()
    {
        if (shouldScrollUp)
        {
            ScrollUp();
        }
        if (shouldScrollDown)
        {
            ScrollDown();
        }
    }

    private void ScrollUp()
    {
        ScrollTo(transform.position.y + ScrollSpeed);
    }

    private void ScrollDown()
    {
        ScrollTo(transform.position.y - ScrollSpeed);
    }

    private void ScrollTo(float height)
    {
        if (height < minHeight)
        {
            height = minHeight;
        }

        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
