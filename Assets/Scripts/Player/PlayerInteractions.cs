﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour, IHitabble
{
    [SerializeField] PlayerStats stats;
    [SerializeField] PlayerMovement movement;

    public void ReciveDamage(float amountDamage, float knockBackForce, Vector2 posAttacker)
    {
        stats.lifeAmount -= amountDamage;
        Vector2 difference = new Vector2(transform.position.x, transform.position.y) - posAttacker;
        movement.ImpulseAttack(difference, knockBackForce);
    }
}
