using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercombat : MonoBehaviour
{

    public Animator animator;

    public Transform boomrang;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack(); 
        }   
    }  
void Attack()
    {
        // play an Attack animation  
        animator.SetTrigger("Attack");
        animator.SetTrigger("boomrang");
        // Detact enemies in range of attack 

        // Damage them
       
    }

    void OnDrawGizmosSelected()
    { 
        if(attackPoint == null) 
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
