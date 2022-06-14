using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour
{

    public float SpeedMultiplier;
    public float jumpImpulse;

    public Rigidbody2D objRigidBody;
    private bool isJumping;
    public ParticleSystem playerMovementParticleSystem;
    public ParticleSystem.MainModule particleMain;
    public GameObject playerHurtParticle;

    public int currentHeart;
    public int maxHeart = 3;
    public UIManager uıManager;

    private int collectedCoin;

    // Start is called before the first frame update
    void Start()
    {
        objRigidBody = GetComponent<Rigidbody2D>();
        particleMain = playerMovementParticleSystem.main;
        currentHeart = maxHeart;

        uıManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
            playerMovementParticleSystem.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Terrain"))
        {
            playerMovementParticleSystem.Play();
        }else if (col.gameObject.CompareTag("Enemy"))
        {
            GameObject hurtObject = Instantiate(playerHurtParticle,transform.position,Quaternion.identity,null);
            UpdateHealth(-1);
        }
    }

    public void RefreshHealth()
    {
        UpdateHealth(maxHeart-currentHeart);
    }

    public void UpdateHealth(int newHealth)
    {
        currentHeart += newHealth;
        if (currentHeart <= 0)
        {
            uıManager.OpenLoseScreen();
        }
        uıManager.UpdateHeartImage(currentHeart);
    }

    public void CoinCollection(int coinAmount)
    {
        collectedCoin += coinAmount;
        uıManager.UpdateCoinCollectionText(collectedCoin);
    }

    private void FixedUpdate()
    {

        if (isJumping)
        {
            Vector3 upMovement = new Vector3(0, jumpImpulse, 0);
            objRigidBody.AddForce(upMovement,ForceMode2D.Impulse);
            isJumping = false;
        }
        float horizontalmovement = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalmovement, 0, 0);
        objRigidBody.AddForce(movement*SpeedMultiplier, ForceMode2D.Force);
    }
}
