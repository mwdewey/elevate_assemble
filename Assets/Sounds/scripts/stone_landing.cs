using UnityEngine;
using System.Collections;

public class stone_landing : MonoBehaviour {

    AudioSource source;
    public AudioClip clip;

    public int maxPlays = 1;
    int currentPlays = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (currentPlays < maxPlays)
        {
            float vol = Random.Range(0, 10f);
            source.pitch = Random.Range(0.75f, 1.25f);

            source.PlayOneShot(clip, vol);
            currentPlays++;
        }
    }
	
}
