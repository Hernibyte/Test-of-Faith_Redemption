﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("ATTACK NEEDS")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float rangeMelee;
    [SerializeField] float impulsePerAttack;

    PlayerStats playerData;
    PlayerMovement movementPlayer;

    void Start()
    {
        movementPlayer = GetComponent<PlayerMovement>();
        playerData = GetComponent<PlayerStats>();
    }

    public void Attack()
    {
        Vector2 directionImpulse = attackPoint.position - transform.position;
        movementPlayer.ImpulseAttack(directionImpulse, impulsePerAttack);

        Debug.Log("ATAQUE Meleeee");

        Collider2D [] collisions = Physics2D.OverlapCircleAll(new Vector2(attackPoint.position.x, attackPoint.position.y), rangeMelee);
        foreach (Collider2D coll in collisions)
        {
            IHitabble hit = coll.GetComponent<IHitabble>();
            if(hit != null)
            {
                hit.ReciveDamage(playerData.damageMeleeAttack, playerData.knockbackMelee, attackPoint.position);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, rangeMelee);
    }
}
