using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollableElement : MonoBehaviour
{
    public float ScrollSpeed = 0.5f;
    public bool Autoscroll = false;
    public float AutoscrollDelay = 5;
    public float ElementHeight = 0;

    private bool shouldScrollUp;
    private bool shouldScrollDown;

    private ScoreKeeper scoreKeeper;
    private float minHeight;
    private float maxHeight;

    private float remainingDelay;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        minHeight = transform.position.y;
        maxHeight = minHeight + scoreKeeper.ScoreEntries.Count * ElementHeight;

        remainingDelay = AutoscrollDelay;
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

            if (Autoscroll)
            {
                remainingDelay = AutoscrollDelay;
            }
        }
        else if (netInput < 0)
        {
            shouldScrollUp = false;
            shouldScrollDown = true;

            if (Autoscroll)
            {
                remainingDelay = AutoscrollDelay;
            }
        }
        else if (Autoscroll && remainingDelay < 0)
        {
            shouldScrollUp = true;
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

        if (Autoscroll && !shouldScrollUp && !shouldScrollDown)
        {
            remainingDelay -= Time.deltaTime;
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
        if (height > maxHeight)
        {
            height = maxHeight;
        }

        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
