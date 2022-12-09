using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float BeatsPerMinute;
    public int CurrentBeat = 1;

    public AudioSource Track;

    private const int _beatsPerMeasure = 4;
    private float _timeToNextBeat;
    private bool _paused = false;

    public float BeatProgress => _timeToNextBeat / _beatTime;

    private float _beatTime => 60f / BeatsPerMinute;

    private void Start()
    {
        _timeToNextBeat = _beatTime;
    }

    private void Update()
    {
        if (_paused)
        {
            return;
        }

        if (_timeToNextBeat <= 0)
        {
            Beat();
        }
        _timeToNextBeat -= Time.deltaTime;
    }

    public void Pause()
    {
        _paused = true;
        Track.Pause();
    }

    public void Play()
    {
        _paused = false;
        Track.Play();
    }

    private void Beat()
    {
        _timeToNextBeat += _beatTime;

        CurrentBeat++;
        if (CurrentBeat > _beatsPerMeasure)
        {
            CurrentBeat = 1;
        }
    }
}
