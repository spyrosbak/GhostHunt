using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Equipment")]
    [SerializeField] private GameObject emf;
    [SerializeField] private GameObject photoCamera;
    [SerializeField] private GameObject spiritBox;

    [Header("UI")]
    [SerializeField] private GameObject emfPanel;
    [SerializeField] private GameObject photoCmaeraPanel;
    [SerializeField] private GameObject spiritBoxPanel;
    [SerializeField] private GameObject objective1Text;
    [SerializeField] private GameObject objective2Text;
    [SerializeField] private GameObject objective3Text;
    [SerializeField] private Button emfIcon;
    [SerializeField] private Button cameraIcon;
    [SerializeField] private Button spiritBoxIcon;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject successPanel;

    private GhostSpawner ghostSpawner;
    private float timer = 60;
    private bool timeOut;
    private bool win;

    private void Awake()
    {
        ghostSpawner = FindFirstObjectByType<XROrigin>().GetComponent<GhostSpawner>();
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        PromptEMF();
    }

    void Update()
    {
        if (!win)
        {
            Countdown();
        }

        if (emf.activeInHierarchy)
        {
            if(ghostSpawner.ghostsFoundCount > 1)
            {
                PromptCamera();
            }
        }
        
        if(photoCamera.activeInHierarchy)
        {
            ghostSpawner.ShowGhosts();

            int capturedGhosts = 0;
            foreach (GameObject ghost in ghostSpawner.spawnedObjects)
            {
                if (ghost.GetComponent<Ghost>().isCaptured)
                {
                    capturedGhosts++;
                    if (capturedGhosts == ghostSpawner.spawnedObjects.Count)
                    {
                        PromptSpiritBox();
                    }
                }
            }
        }
        else
        {
            foreach (GameObject ghost in ghostSpawner.spawnedObjects)
            {
                if (!ghost.GetComponent<Ghost>().isCaptured)
                {
                    ghost.gameObject.SetActive(false);
                }
            }
        }
        
        if (spiritBox.activeInHierarchy)
        {
            int bustedGhosts = 0;
            foreach (GameObject ghost in ghostSpawner.spawnedObjects)
            {
                if (ghost.GetComponent<Ghost>().isBusted)
                {
                    bustedGhosts++;
                    if (bustedGhosts == ghostSpawner.spawnedObjects.Count && !timeOut)
                    {
                        win = true;
                        successPanel.SetActive(true);
                        spiritBox.SetActive(false);
                    }
                }
            }
        }
    }

    private void PromptEMF()
    {
        objective1Text.SetActive(true);
        emfIcon.interactable = true;
    }

    private void PromptCamera()
    {
        objective1Text.SetActive(false);
        emfIcon.interactable = false;

        objective2Text.SetActive(true);
        cameraIcon.interactable = true;
    }

    private void PromptSpiritBox()
    {
        objective2Text.SetActive(false);
        cameraIcon.interactable = false;

        objective3Text.SetActive(true);
        spiritBoxIcon.interactable = true;
    }

    public void SwitchToEMF()
    {
        emfPanel.SetActive(true);
        emf.SetActive(true);
    }

    public void SwitchToCamera()
    {
        ghostSpawner.canSpawn = false;

        emfPanel.SetActive(false);
        emf.SetActive(false);

        photoCmaeraPanel.SetActive(true);
        photoCamera.SetActive(true);
    }

    public void SwitchToSpiritBox()
    {
        photoCmaeraPanel.SetActive(false);
        photoCamera.SetActive(false);

        spiritBoxPanel.SetActive(true);
        spiritBox.SetActive(true);
    }

    private void Countdown()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F0");

        if(timer <= 0)
        {
            timeOut = true;
            gameOverPanel.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}