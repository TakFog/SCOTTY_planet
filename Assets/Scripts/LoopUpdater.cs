using UnityEngine;
using System.Collections;

public class LoopUpdater : MonoBehaviour
{
    public AudioClip loop;
    private AudioSource src;

    // Use this for initialization
    void Start()
    {
        src = (AudioSource)GetComponent(typeof(AudioSource));
    }

    // Update is called once per frame
    void Update()
    {
        if (!src.isPlaying)
        {
            src.clip = loop;
            src.loop = true;
            src.Play();
            this.enabled = false;
        }

    }
}