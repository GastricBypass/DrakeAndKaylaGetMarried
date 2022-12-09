using UnityEngine;

public class BeatAnimator : MonoBehaviour
{
    public MusicManager Music;
    public Animator Animator;

    private void Start()
    {
        if (Music == null)
        {
            Music = FindObjectOfType<MusicManager>();
        }

        if (Animator == null)
        {
            Animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        Animator.speed = Music.BeatsPerMinute / 180f; // animator plays at 12 fps or 180bpm at speed 1
    }
}
