using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI coinCountText;
    public PlayerMovementScript currentPlayer;

    public GameObject winScreen;
    public GameObject loseScreen;
    public Image[] heartImages;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        PlayerDataManager.Instance.onCoinDataChange += UpdateCoinCollectionText;
        PlayerDataManager.Instance.onLifeDataChange += UpdateHeartImage;
    }

    public void UpdateHeartImage(int Index)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < Index)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }
    
    public void UpdateCoinCollectionText(int coinAmount)
    {
        coinCountText.text = PlayerDataManager.Instance.playerData.collectedCoin.ToString();
    }

    public void OpenWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void OpenLoseScreen()
    {
        loseScreen.SetActive(true);
    }
    
}
