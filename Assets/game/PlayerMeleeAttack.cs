using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityTakeDamage {
    void takeDamage(float damage, float knock, Vector3 dealerDir);
}

public class PlayerMeleeAttack : MonoBehaviour
{
    Collider2D col;
    playerMovement playerMovementScript;

    float cooldown = 0.0f;
    float attackDamage = 10.0f;
    float attackKonckback = 1.5f;

    void Start() {
        col = GetComponent<Collider2D>();
        playerMovementScript = GetComponentInParent<playerMovement>();
    }

    void Update() {
        cooldown = Math.Max(cooldown - Time.deltaTime, 0.0f);

        if (Input.GetKeyDown("x") && cooldown == 0.0f) {
            cooldown = 0.5f;
            
            //Atacar
            foreach (GameObject attackable in GameObject.FindGameObjectsWithTag("canTakeDamage")) {
                Collider2D attacableCollider = attackable.GetComponent<Collider2D>();

                if (attacableCollider.IsTouching(col)) {
                    IEntityTakeDamage script = attackable.GetComponent<IEntityTakeDamage>();

                    script.takeDamage(attackDamage, attackKonckback, playerMovementScript.dir);
                }
            }
        }

        //Posição da área de ataque
        transform.localPosition = playerMovementScript.dir * 0.14f;
    }
}
