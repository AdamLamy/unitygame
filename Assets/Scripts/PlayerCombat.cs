using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform Meleepoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                melee();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void melee()
    {
        //animation
        animator.SetTrigger("melee");
        //Debug.Log("animation played");

        //detect enemies
        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(Meleepoint.position, attackRange, enemyLayers);

        //damage
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<meleeEnemy>().TakeDamage(attackDamage);
            //Debug.Log("We hit " + enemy.name);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (Meleepoint == null)
            return;

        Gizmos.DrawWireSphere(Meleepoint.position, attackRange);

    }
}
