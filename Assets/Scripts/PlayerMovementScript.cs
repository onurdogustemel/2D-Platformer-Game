using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    public UnityAction<int> onCoinCollection;
    public UnityAction<int> onHealthChanged;

    public Animator animating;
    
    public AudioSource playerAudioSource;

    public AudioClip JumpAudio;
    public AudioClip HitByEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animating.GetComponent<Animator>();
        objRigidBody = GetComponent<Rigidbody2D>();
        particleMain = playerMovementParticleSystem.main;
        PlayerDataManager.Instance.playerData.currentHeart = PlayerDataManager.Instance.playerData.maxHeart;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
            playerMovementParticleSystem.Stop();
            animating.SetTrigger("Jumping");
            playerAudioSource.PlayOneShot(JumpAudio);
        }

        if (Input.GetKey(KeyCode.LeftArrow) 
            || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                animating.SetBool("isMovingRight",true);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                animating.SetBool("isMovingLeft",true);
            }
        }
        else
        {
            animating.SetBool("isMovingRight",false);
            animating.SetBool("isMovingLeft",false);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Terrain"))
        {
            playerMovementParticleSystem.Play();
            animating.SetTrigger("ReturnToIdle");
        }else if (col.gameObject.CompareTag("Enemy"))
        {
            GameObject hurtObject = Instantiate(playerHurtParticle,transform.position,Quaternion.identity,null);
            playerAudioSource.PlayOneShot(HitByEnemy);
            UpdateHealth(-1);
        }
    }

    public void RefreshHealth()
    {
        UpdateHealth(PlayerDataManager.Instance.playerData.maxHeart-PlayerDataManager.Instance.playerData.currentHeart);
    }

    public void UpdateHealth(int newHealth)
    {
        onHealthChanged?.Invoke(newHealth);
    }

    public void CoinCollection(int coinAmount)
    {
        onCoinCollection?.Invoke(coinAmount);
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

        if (objRigidBody.velocity.y < -2f)
        {
            animating.SetTrigger("Falling");
        }
    }
}
