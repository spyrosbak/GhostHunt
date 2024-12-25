using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBox : MonoBehaviour
{
    private GhostSpawner ghostSpawner;
    private AudioSource highPitchSound;

    private void Awake()
    {
        ghostSpawner =  FindFirstObjectByType<GhostSpawner>();
    }

    void Start()
    {
        highPitchSound = GetComponent<AudioSource>();

        StartCoroutine(BustGhosts());
        highPitchSound.Play();
    }

    private IEnumerator BustGhosts()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i <= ghostSpawner.spawnedObjects.Count; i++)
        {
            ghostSpawner.spawnedObjects[i].GetComponent<Ghost>().GetBusted();
        }

        highPitchSound.Stop();
    }
}