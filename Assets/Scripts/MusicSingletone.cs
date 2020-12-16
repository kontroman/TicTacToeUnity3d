using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingletone : MonoBehaviour
{
    public static MusicSingletone instance = null;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitializeManager();
    }

    private void InitializeManager()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
