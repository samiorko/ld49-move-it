using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> clips;

    public AudioSource source1;
    public AudioSource source2;


    private AudioSource _currentSource;
    
    private float _lastQueuedTime;

    // Update is called once per frame
    void Update()
    {
        if (!source1.isPlaying)
        {
            source1.clip = clips[0];
            source1.Play();
            _currentSource = source1;
        }

        var time = source1.time;

        if (_lastQueuedTime > time)
        {
            
        }
        
    }
    
    
}
