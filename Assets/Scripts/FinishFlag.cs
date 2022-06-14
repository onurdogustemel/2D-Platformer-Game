using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{

    public UIManager uiManager;
    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            uiManager.OpenWinScreen();
        }
    }
}
