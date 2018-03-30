using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        // todo handle more than two audio sources through code
        // todo handle music restart on crash
        AudioSource audioSource = GetComponent<AudioSource>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            audioSource.Stop();
            Destroy(this.gameObject);
        }
    }
}
