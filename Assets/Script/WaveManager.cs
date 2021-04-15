using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    private Wave[] waves;

    [SerializeField] private Transform[] repaires;
    [SerializeField] private int currentWaveIndex = 0;

    public Image imgGoNext;

    private void Awake()
    {
        waves = GetComponentsInChildren<Wave>();

        foreach (Wave wave in waves)
        {
            wave.gameObject.SetActive(false);
            wave.waveManager = this;
            wave.repaires = repaires;
        }

        if (currentWaveIndex < waves.Length)
            waves[currentWaveIndex].gameObject.SetActive(true);

        imgGoNext.enabled = false;
    }

    public void NextWave()
    {
        waves[currentWaveIndex].gameObject.SetActive(false);

        currentWaveIndex++;

        if (currentWaveIndex < waves.Length)
        {
            waves[currentWaveIndex].gameObject.SetActive(true);
            StartCoroutine(StartClignPanel());
        }
    }

    private IEnumerator StartClignPanel()
    {
        while (!waves[currentWaveIndex].waveStarted)
        {
            imgGoNext.enabled = true;

            yield return new WaitForSeconds(.5f);

            imgGoNext.enabled = false;

            yield return new WaitForSeconds(.5f);
        }
    }
}
