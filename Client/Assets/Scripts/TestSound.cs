using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioClip audioClip;
    public AudioClip audioClip2;
    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audioClip);
        audio.PlayOneShot(audioClip2);
        float lifeTime = Mathf.Max(audioClip.length, audioClip2.length);

        GameObject.Destroy(gameObject, 0.25f);
    }
}
