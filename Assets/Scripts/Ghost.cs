using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
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
        //animation
    }

    public void GetBusted()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
        isBusted = true;
        gameObject.SetActive(false);
    }
}