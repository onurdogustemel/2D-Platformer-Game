using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinCountText;

    public GameObject winScreen;
    public GameObject loseScreen;
    public Image[] heartImages;

    public void UpdateHeartImage(int index)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < index)
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
        coinCountText.text = coinAmount.ToString();
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
