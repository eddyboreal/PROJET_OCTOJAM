using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip menu_music;
    
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        
    }
}
