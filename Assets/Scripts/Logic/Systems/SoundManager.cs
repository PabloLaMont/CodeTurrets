using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource spawnLineAS;
    public AudioSource compileAS;
    public AudioSource compileGPUAS;
    public AudioSource levelUpAS;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySpawnLineSound()
    {
        spawnLineAS.Play();
    }

    public void PlayCompileSound()
    {
        compileAS.Play();
    }

    public void PlayCompileGPUSound()
    {
        compileGPUAS.Play();
    }

    public void PlayLevelUpSound()
    {
        levelUpAS.Play();
    }
}

