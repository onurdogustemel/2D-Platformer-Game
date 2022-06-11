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

    public float currentHealth;
    public float maxHealth = 200f;
    public Image healthImage;
    public TextMeshProUGUI coinCountText;

    private int collectedCoin;

    // Start is called before the first frame update
    void Start()
    {
        objRigidBody = GetComponent<Rigidbody2D>();
        particleMain = playerMovementParticleSystem.main;
        collectedCoin = 0;
        currentHealth = maxHealth;
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
            UpdateHealth(-20f);
        }
    }

    public void RefreshHealth()
    {
        UpdateHealth(maxHealth - currentHealth);
    }

    public void UpdateHealth(float newHealth)
    {
        currentHealth += newHealth;
        healthImage.fillAmount = currentHealth / maxHealth;
    }

    public void CoinCollection(int coinAmount)
    {
        collectedCoin += coinAmount;
        coinCountText.text = collectedCoin.ToString();
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
