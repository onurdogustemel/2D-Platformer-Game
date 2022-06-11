using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinParticle : MonoBehaviour
{
    public int coinValue;
    public GameObject coinParticle;
    public void SpawnParticle()
    {
        GameObject spawnedObject = Instantiate(coinParticle,transform.position,Quaternion.identity,null);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SpawnParticle();
            col.GetComponent<PlayerMovementScript>().CoinCollection(coinValue);
            Destroy(gameObject);
        }
    }
}
