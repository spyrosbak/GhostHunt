using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(ARPlaneManager))]
public class GhostSpawner : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;
    [SerializeField] private GameObject ghost;
    [SerializeField] private TextMeshProUGUI spawnedObjectsText;
    public List<GameObject> spawnedObjects = new List<GameObject>();
    public bool canSpawn = true;
    public int ghostsFoundCount = 0;

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();

        arPlaneManager.planesChanged += PlaneFound;
    }

    private void PlaneFound(ARPlanesChangedEventArgs args)
    {
        if(args.added != null)
        {
            foreach(ARPlane plane in args.added)
            {
                plane.gameObject.SetActive(false);

                if (canSpawn)
                {
                    Vector3 spawnPos = new Vector3(plane.transform.position.x, plane.transform.position.y + 0.5f, plane.transform.position.z);
                    GameObject spawnedObject = Instantiate(ghost, spawnPos, Quaternion.identity);
                    spawnedObject.SetActive(false);
                    ghostsFoundCount++;
                    spawnedObjects.Add(spawnedObject);
                    spawnedObjectsText.text = "Uknown Entities Detected: " + ghostsFoundCount;
                }
            }   
        }
    }

    public void ShowGhosts()
    {
        for(int i = 0; i < spawnedObjects.Count; i++)
        {
            spawnedObjects[i].SetActive(true);
        }
    }
}