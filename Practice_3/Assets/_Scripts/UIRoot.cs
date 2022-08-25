using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UIRoot : MonoBehaviour
{
    [SerializeField]
    TMP_Text deathCountUI; 
    [SerializeField]
    TMP_Text waveUI;
    int curWave=0;
    int deathCount=0;
    void Start()
    {
        deathCountUI.gameObject.SetActive(false);
        waveUI.gameObject.SetActive(false);
    }
    public void OnGameStarted()
    {
        deathCountUI.gameObject.SetActive(true);
        deathCountUI.text = string.Format("X{0}", deathCount);
    }

    public void DeathCounting()
    {
        deathCount++;
        deathCountUI.text = string.Format("X{0}",deathCount);
    }

    public void OnWaveChanged()
    {
        curWave++;
        waveUI.gameObject.SetActive(true);
        waveUI.text = string.Format("Wave {0}", curWave);
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        waveUI.gameObject.SetActive(false);
    }
}