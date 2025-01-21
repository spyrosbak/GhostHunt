using System.Collections;
using UnityEngine;

public class GhostDetection : MonoBehaviour
{
    [SerializeField] private Material[] signalIntensityMat;
    [SerializeField] private MeshRenderer mat;
    private GhostSpawner spawner;
    private AudioSource beepSound;
    private float frequency;

    private void Awake()
    {
        spawner = FindFirstObjectByType<GhostSpawner>();
        beepSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(Beep());
    }

    void Update()
    {
        DetectorSignal();
    }

    private void DetectorSignal()
    {
        if (spawner.ghostsFoundCount <= 0)
        {
            mat.material = signalIntensityMat[0];
            frequency = 2f;
        }
        else if (spawner.ghostsFoundCount == 1)
        {
            mat.material = signalIntensityMat[1];
            frequency = 1f;
        }
        else if (spawner.ghostsFoundCount == 2)
        {
            mat.material = signalIntensityMat[2];
            frequency = 0.75f;
        }
        else if (spawner.ghostsFoundCount == 3)
        {
            mat.material = signalIntensityMat[3];
            frequency = 0.5f;
        }
        else if (spawner.ghostsFoundCount >= 4)
        {
            mat.material = signalIntensityMat[4];
            frequency = 0.25f;
        }
    }

    private IEnumerator Beep()
    {
        while (enabled)
        {
            beepSound.Play();
            yield return new WaitForSeconds(frequency);
        }
    }
}