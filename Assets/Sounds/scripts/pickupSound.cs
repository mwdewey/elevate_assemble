using UnityEngine;
using System.Collections;

public class pickupSound : MonoBehaviour {

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
        if (col.gameObject.GetComponent<ResourceBehavior>() == true)
        {
            float vol = Random.Range(0, 5f);
            source.pitch = Random.Range(0.75f, 1.25f);
            //float vol = 1f;
            //source.pitch = 1f;

            source.PlayOneShot(clip, vol);
            currentPlays++;
        }
    }
}
