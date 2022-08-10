using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    public AudioSource winnerAudioSource;
    private void Start()
    {
        winnerAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            UIManager.Instance.OpenWinScreen();
            winnerAudioSource.Play();
        }
    }
}
