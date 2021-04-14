using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private Wave[] waves;

    [SerializeField] private int currentWaveIndex = 0;

    private void Awake()
    {
        waves = GetComponentsInChildren<Wave>();

        foreach (Wave wave in waves)
        {
            wave.gameObject.SetActive(false);
            wave.waveManager = this;
        }

        if (currentWaveIndex < waves.Length)
            waves[currentWaveIndex].gameObject.SetActive(true);
    }

    public void NextWave()
    {
        waves[currentWaveIndex].gameObject.SetActive(false);

        currentWaveIndex++;

        if (currentWaveIndex < waves.Length)
        {
            waves[currentWaveIndex].gameObject.SetActive(true);
        }
    }
}
