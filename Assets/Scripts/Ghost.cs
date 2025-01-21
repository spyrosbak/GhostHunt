using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private ParticleSystem stunnedVFX;
    [SerializeField] private ParticleSystem bustedVFX;
    [SerializeField] private MeshRenderer graphic;

    public bool isCaptured;
    public bool isBusted;

    void Start()
    {
        float yrot = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, yrot, 0);
    }

    public void GetCaptured()
    {
        isCaptured = true;
        stunnedVFX.Play();
    }

    public void GetBusted()
    {
        bustedVFX.Play();
        stunnedVFX.Stop();
        graphic.enabled = false;

        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);

        isBusted = true;
    }
}