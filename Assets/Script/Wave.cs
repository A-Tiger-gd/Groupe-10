using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public struct WaveStep
    {
        public float spawnTime;
        public enemie[] enemies;
    }

    private GameObject player;
    private bool waveStarted = false;
    private bool camMove = false;
    private Vector3 sCam;
    private bool canStartNextStep = true;

    [HideInInspector] public WaveManager waveManager;

    [Range(0f, 9f)] public float xWaveStart = 0f;
    public float cameraTransitionSpeed = 2f;
    [Space]
    public WaveStep[] steps;
    public int currentStepIndex = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        foreach (WaveStep waveStep in steps)
        {
            foreach (enemie enemie in waveStep.enemies)
            {
                enemie.gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        if (currentStepIndex < 0 || currentStepIndex >= steps.Length)
            currentStepIndex = 0;
    }

    private void Update()
    {
        /* Before wave start */
        if (!waveStarted && player.transform.position.x >= transform.position.x - xWaveStart)
        {
            waveStarted = true;
            camMove = true;
            sCam = Camera.main.transform.position;
        }

        if (camMove && Camera.main.transform.position.x < transform.position.x)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, sCam.y, sCam.z), Time.deltaTime * 2f);
        }
        else if (camMove && Camera.main.transform.position.x >= transform.position.x)
        {
            camMove = false;
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }

        /* Wave already started */
        if (waveStarted && canStartNextStep)
        {
            if (currentStepIndex >= steps.Length)
            {
                waveManager.NextWave();
            }
            else
            {
                StartCoroutine(SpawnNextWave());
            }
        }

    }

    IEnumerator SpawnNextWave()
    {
        canStartNextStep = false;

        yield return new WaitForSeconds(steps[currentStepIndex].spawnTime);

        foreach (enemie enemie in steps[currentStepIndex].enemies)
        {
            enemie.gameObject.SetActive(true);
            // enemie.GoToStartPos();
        }

        currentStepIndex++;
        canStartNextStep = true;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - xWaveStart, transform.position.y - 5f, 0f), new Vector3(transform.position.x - xWaveStart, transform.position.y - 1f, 0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector2(18f, 10f));
    }
}
