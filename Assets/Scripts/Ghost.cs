using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private ParticleSystem stunnedVFX;
    [SerializeField] private ParticleSystem bustedVFX;

    public bool isCaptured;
    public bool isBusted;

    // Start is called before the first frame update
    void Start()
    {
        float yrot = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, yrot, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCaptured()
    {
        isCaptured = true;
        stunnedVFX.Play();
    }

    public void GetBusted()
    {
        isBusted = true;
        bustedVFX.Play();
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1.1f);

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}