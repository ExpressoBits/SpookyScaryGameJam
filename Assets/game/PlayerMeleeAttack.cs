using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    Collider2D col;
    playerMovement moveScript;

    float cooldown = 1.0f;
    float attackDuration = 0.3f;

    float attackDamage = 10.0f;
    float attackKonckback = 5.0f;

    void Start() {
        col = GetComponent<Collider2D>();
        moveScript = GetComponent<playerMovement>();
    }

    void Update() {
        cooldown = Math.Max(cooldown - Time.deltaTime, 0.0f);
        attackDuration = Math.Max(attackDuration - Time.deltaTime, 0.0f);

        if (Input.GetKeyDown("x") && cooldown == 0.0f) {
            cooldown = 1.0f;
            attackDuration = 0.3f;
        }

        if (attackDuration > 0.0f) {
            
        }
        
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "canTakeDamage") {
            IEntityTakeDamage damageTaker = collider.GetComponent<IEntityTakeDamage>();

            damageTaker.takeDamage(attackDamage, attackKonckback, moveScript.dirX);
        }
    }
}
