using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageArea : MonoBehaviour
{
    [SerializeField]
    private float deactivateWaitTime = 0.2f;
    private float deactivateTimer;

    [SerializeField]
    private LayerMask playerLayer;


    private bool canDealDamage;

    [SerializeField]
    private float damagedAmount = 4f;

    private PlayerHealth playerHealth;


    private void Awake()
    {
        playerHealth = GameObject.FindWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerHealth>();

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 1f, playerLayer))
        {
            if(canDealDamage)
            {
                canDealDamage = false;
                playerHealth.TakeDamage(damagedAmount);
                Debug.Log("Give Damage to Player");
            }
            
        }
    }

    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    public void ResetDeactiveTimer()
    {
        canDealDamage = true;
        deactivateTimer = Time.time + deactivateWaitTime;
    }
}
