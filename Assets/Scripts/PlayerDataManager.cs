using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataManager : MonoBehaviour
{
    private static PlayerDataManager _instance;
    public UnityAction<int> onCoinDataChange;
    public UnityAction<int> onLifeDataChange;
    public static PlayerDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerDataManager>();

            }

            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = nameof(PlayerDataManager);
                _instance = obj.AddComponent<PlayerDataManager>();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }



    public PlayerData playerData = new PlayerData();

    public PlayerMovementScript currentPlayer;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        currentPlayer = FindObjectOfType<PlayerMovementScript>();
        currentPlayer.onCoinCollection += onCoinChanged;
        currentPlayer.onHealthChanged = onHealthChange;
    }

    public void onCoinChanged(int coinAmount)
    {
        playerData.collectedCoin += coinAmount;
        onCoinDataChange?.Invoke(playerData.collectedCoin);
    }

    public void onHealthChange(int currentHealth)
    {
        playerData.currentHeart += currentHealth;
        onLifeDataChange?.Invoke(playerData.currentHeart);
        if (playerData.currentHeart <= 0)
        {
            UIManager.Instance.OpenLoseScreen();
        }
    }
}
