using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundDelayed : MonoBehaviour
{

    // seconds
    public int delay;
    public AudioClip clip;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = clip;
        _audioSource.PlayDelayed(delay);
        Invoke(nameof(Clear), delay + clip.length);
    }

    void Clear()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
