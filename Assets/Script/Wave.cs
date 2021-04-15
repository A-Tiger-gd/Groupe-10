using System.Collections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public struct WaveStep
    {
        public float spawnTime;
        public enemie[] enemies;
        public bool beated;
    }

    private deplacement player;
    [HideInInspector] public bool waveStarted = false;
    private bool camMove = false;
    private Vector3 sCam;
    private bool canStartNextStep = true;

    [HideInInspector] public WaveManager waveManager;
    [HideInInspector] public Transform[] repaires;

    [Range(0f, 9f)] public float xWaveStart = 0f;
    public float cameraTransitionSpeed = 2f;
    [Space]
    public WaveStep[] steps;
    public int currentStepIndex = 0;

    private void Awake()
    {
        player = FindObjectOfType<deplacement>();

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
            player.canMove = false;
            player.fixedCam = true;
        }

        if (camMove && Camera.main.transform.position.x < transform.position.x - 0.1f)
        {
            Vector3 nVec = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, sCam.y, sCam.z), Time.deltaTime * 2f);
            Camera.main.transform.position = nVec;
        }
        else if (camMove && Camera.main.transform.position.x >= transform.position.x - 0.1f)
        {
            camMove = false;
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

            float distance = Camera.main.transform.position.x - sCam.x;

            for (int i = 0; i < repaires.Length; ++i)
            {
                repaires[i].position = new Vector2(repaires[i].position.x + distance, repaires[i].position.y);
            }

            player.canMove = true;
        }

        /* Wave already started */
        if (waveStarted && canStartNextStep && currentStepIndex < steps.Length)
        {
            StartCoroutine(SpawnNextWave());
        }

        if (waveStarted)
        {
            if (IsWaveEnded())
            {
                player.fixedCam = false;
                waveManager.NextWave();
            } 
        }

    }

    private bool IsWaveEnded()
    {
        bool isWaveEnded = false;
        bool nBreak = false;

        for (int i = 0; i < steps.Length; i++)
        {
            if (!steps[i].beated)
            {
                foreach (enemie enemie in steps[i].enemies)
                {
                    if (enemie != null)
                    {
                        nBreak = true;
                        isWaveEnded = false;
                        break;
                    }
                }

                if (!nBreak)
                {
                    steps[i].beated = true;
                }
            }
            else
            {
                isWaveEnded = true;
            }

            if (nBreak)
                break;
        }

        return isWaveEnded;
    }

    IEnumerator SpawnNextWave()
    {
        canStartNextStep = false;

        yield return new WaitForSeconds(steps[currentStepIndex].spawnTime);

        foreach (enemie enemie in steps[currentStepIndex].enemies)
        {
            enemie.gameObject.SetActive(true);
            enemie.StartGoToStartPos();

            if (currentStepIndex == 0)
            {
                enemie.canShoot = false;
            }

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
