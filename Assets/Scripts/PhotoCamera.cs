using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PhotoCamera : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject middleOfScreen;
    private AudioSource clickSound;

    // Start is called before the first frame update
    void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeShot()
    {
        Ray ray = arCamera.ScreenPointToRay(middleOfScreen.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject obj = hit.transform.gameObject;
            if (obj.CompareTag("Target"))
            {
                clickSound.Play();
                obj.GetComponent<Ghost>().GetCaptured();
            }
        }
    }
}