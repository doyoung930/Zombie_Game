using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    private bool isDead = false;
    
    
    public bool IsDead () { return isDead; }
    
    public void TakeDamage(float damage)
    {
        //GetComponent<EnemyAI>().OnDamageTaken();
        
        BroadcastMessage("OnDamageTaken");
        
        hitPoints -= damage;

        if (hitPoints <= 0)
        {

            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
    }
}
