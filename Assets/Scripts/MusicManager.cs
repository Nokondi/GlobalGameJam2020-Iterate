using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    AudioSource audio;
    public List<AudioClip> clips;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogWarning("There are more than one MusicManagers" +
                " in the scene. Destroying this...");
        }
        else Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audio.isPlaying == false)
        {
            audio.clip = clips[Random.Range(0, clips.Count)];
            audio.Play();
        }
    }
}
